using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Svc.DTOs.User;

public class RegisterDto : LoginDto
{
  [Required]
  public string FirstName { get; set; } = default!;

  [Required]
  public string LastName { get; set; } = default!;

  [Required]
  public string Role { get; set; } = default!;
}
