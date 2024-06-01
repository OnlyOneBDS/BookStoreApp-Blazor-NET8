using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.Providers;

public class AuthStateProvider : AuthenticationStateProvider
{
  private readonly ILocalStorageService _localStorage;
  private readonly JwtSecurityTokenHandler _tokenHandler;

  public AuthStateProvider(ILocalStorageService localStorage)
  {
    _localStorage = localStorage;
    _tokenHandler = new JwtSecurityTokenHandler();
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    var user = new ClaimsPrincipal(new ClaimsIdentity());
    var savedToken = await _localStorage.GetItemAsync<string>("authToken");

    if (savedToken == null)
    {
      return new AuthenticationState(user);
    }

    var tokenContent = _tokenHandler.ReadJwtToken(savedToken);

    if (tokenContent.ValidTo < DateTime.Now)
    {
      return new AuthenticationState(user);
    }

    var claims = await GetClaimsAsync();

    user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
    return new AuthenticationState(user);
  }

  public async Task LoggedIn()
  {
    var claims = await GetClaimsAsync();
    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
    var authState = Task.FromResult(new AuthenticationState(user));

    NotifyAuthenticationStateChanged(authState);
  }

  public async Task LoggedOut()
  {
    await _localStorage.RemoveItemAsync("authToken");

    var nobody = new ClaimsPrincipal(new ClaimsIdentity());
    var authState = Task.FromResult(new AuthenticationState(nobody));

    NotifyAuthenticationStateChanged(authState);
  }

  private async Task<List<Claim>> GetClaimsAsync()
  {
    var savedToken = await _localStorage.GetItemAsync<string>("authToken");
    var tokenContent = _tokenHandler.ReadJwtToken(savedToken);
    var claims = tokenContent.Claims.ToList();

    claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
    return claims;
  }
}