using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.Svc.Data;
using BookStoreApp.Svc.DTOs.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Svc.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class BooksController : ControllerBase
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BooksController(BookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
      var booksDtos = await _context.Books
        .Include(b => b.Author)
        .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

      //var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);

      return Ok(booksDtos);
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
      var book = await _context.Books
        .Include(b => b.Author)
        .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(b => b.Id == id);

      if (book == null)
      {
        return NotFound();
      }

      return Ok(book);
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
    {
      if (id != bookDto.Id)
      {
        return BadRequest();
      }

      var book = await _context.Books.FindAsync(id);

      if (book == null)
      {
        return NotFound();
      }

      _mapper.Map(bookDto, book);
      _context.Entry(book).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!await BookExistsAsync(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Book>> PostBook(BookCreateDto bookDto)
    {
      var book = _mapper.Map<Book>(bookDto);

      _context.Books.Add(book);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteBook(int id)
    {
      var book = await _context.Books.FindAsync(id);

      if (book == null)
      {
        return NotFound();
      }

      _context.Books.Remove(book);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private async Task<bool> BookExistsAsync(int id)
    {
      return await _context.Books.AnyAsync(e => e.Id == id);
    }
  }
}