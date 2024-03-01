namespace BookStoreApp.Svc.DTOs.Book;

public class BookDto : BaseDto
{
  public int AuthorId { get; set; }
  public string AuthorName { get; set; } = default!;
  public string Title { get; set; } = default!;
  public string Image { get; set; } = default!;
  public decimal Price { get; set; }
}
