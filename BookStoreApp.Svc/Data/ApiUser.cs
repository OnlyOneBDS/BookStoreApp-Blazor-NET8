using Microsoft.AspNetCore.Identity;

namespace BookStoreApp.Svc.Data;

public class ApiUser : IdentityUser
{
  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;
}