namespace BookStoreApp.Svc.DTOs.Book;

public class BookDetailsDto : BaseDto
{
  public int AuthorId { get; set; }
  public string AuthorName { get; set; } = default!;
  public string Title { get; set; } = default!;
  public int Year { get; set; }
  public string Isbn { get; set; } = null!;
  public string Summary { get; set; } = default!;
  public string Image { get; set; } = default!;
  public decimal Price { get; set; }
}