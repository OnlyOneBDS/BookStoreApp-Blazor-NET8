namespace BookStoreApp.Svc.DTOs.User;

public class AuthResponse
{
  public string UserId { get; set; } = default!;
  public string Email { get; set; } = default!;
  public string Token { get; set; } = default!;
}