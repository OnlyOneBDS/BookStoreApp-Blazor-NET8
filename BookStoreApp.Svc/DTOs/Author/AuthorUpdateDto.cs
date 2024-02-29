using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Svc.DTOs.Author;

public class AuthorUpdateDto : BaseDto
{
  [Required]
  [StringLength(50)]
  public string FirstName { get; set; } = default!;

  [Required]
  [StringLength(50)]
  public string LastName { get; set; } = default!;

  [StringLength(250)]
  public string Bio { get; set; } = default!;
}
