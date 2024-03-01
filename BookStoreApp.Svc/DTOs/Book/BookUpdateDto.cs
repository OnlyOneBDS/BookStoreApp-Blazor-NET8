using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Svc.DTOs.Book;

public class BookUpdateDto : BaseDto
{
  [Required]
  [StringLength(50)]
  public string Title { get; set; } = default!;

  [Required]
  [Range(1800, int.MaxValue)]
  public int Year { get; set; }

  [Required]
  public string Isbn { get; set; } = null!;

  [Required]
  [StringLength(250, MinimumLength = 10)]
  public string Summary { get; set; } = default!;

  public string Image { get; set; } = default!;

  [Required]
  [Range(0, int.MaxValue)]
  public decimal Price { get; set; }
}
