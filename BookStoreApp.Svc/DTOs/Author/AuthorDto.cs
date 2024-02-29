namespace BookStoreApp.Svc.DTOs.Author;

public class AuthorDto : BaseDto
{
  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;
  public string Bio { get; set; } = default!;
}
