using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BookStoreApp.Svc.Common;
using BookStoreApp.Svc.Data;
using BookStoreApp.Svc.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApp.Svc.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
  private readonly IConfiguration _configuration;
  private readonly ILogger<AuthController> _logger;
  private readonly IMapper _mapper;
  private readonly UserManager<ApiUser> _userManager;

  public AuthController(IConfiguration configuration, ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
  {
    _configuration = configuration;
    _logger = logger;
    _mapper = mapper;
    _userManager = userManager;
  }

  [HttpPost]
  [Route("login")]
  public async Task<ActionResult<AuthResponse>> Login(LoginDto request)
  {
    _logger.LogInformation($"Login attempt for {request.Email}");

    try
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

      if (user == null || !passwordValid)
      {
        return Unauthorized(request);
      }

      var tokenString = await GenerateToken(user);

      var response = new AuthResponse
      {
        UserId = user.Id,
        Email = request.Email,
        Token = tokenString,
      };

      return response;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"Something went wrong during login for {request.Email}");
      return Problem($"Something went wrong during login for {request.Email}", statusCode: 500);
    }
  }

  [HttpPost]
  [Route("register")]
  public async Task<IActionResult> Register(RegisterDto request)
  {
    try
    {
      var user = _mapper.Map<ApiUser>(request);

      user.UserName = request.Email;

      var result = await _userManager.CreateAsync(user, request.Password);

      if (!result.Succeeded)
      {
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
      }

      await _userManager.AddToRoleAsync(user, "User");
      return Accepted();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"Something went wrong while registering {request.Email}");
      return Problem($"Something went wrong while registering {request.Email}", statusCode: 500);
    }
  }

  private async Task<string> GenerateToken(ApiUser user)
  {
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var roles = await _userManager.GetRolesAsync(user);
    var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
    var userClaims = await _userManager.GetClaimsAsync(user);

    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.Sub, user.UserName),
      new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new(JwtRegisteredClaimNames.Email, user.Email),
      new(CustomClaimTypes.Uid, user.Id)
    }
    .Union(roleClaims)
    .Union(userClaims);

    var token = new JwtSecurityToken(
      issuer: _configuration["JwtSettings:Issuer"],
      audience: _configuration["JwtSettings:Audience"],
      claims: claims,
      expires: DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
      signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}