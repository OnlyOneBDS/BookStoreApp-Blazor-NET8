using AutoMapper;
using BookStoreApp.Svc.Data;
using BookStoreApp.Svc.DTOs.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Svc.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class AuthorsController : ControllerBase
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorsController(BookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
    {
      var authors = await _context.Authors.ToListAsync();
      var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);

      return Ok(authorDtos);
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
    {
      var author = await _context.Authors.FindAsync(id);

      if (author == null)
      {
        return NotFound();
      }

      var authorDto = _mapper.Map<AuthorDto>(author);

      return Ok(authorDto);
    }

    // PUT: api/Authors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
    {
      if (id != authorDto.Id)
      {
        return BadRequest();
      }

      var author = await _context.Authors.FindAsync(id);

      if (author == null)
      {
        return NotFound();
      }

      _mapper.Map(authorDto, author);
      _context.Entry(author).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!await AuthorExistsAsync(id))
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

    // POST: api/Authors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Author>> PostAuthor(AuthorCreateDto authorDto)
    {
      var author = _mapper.Map<Author>(authorDto);

      await _context.Authors.AddAsync(author);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
      var author = await _context.Authors.FindAsync(id);

      if (author == null)
      {
        return NotFound();
      }

      _context.Authors.Remove(author);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private async Task<bool> AuthorExistsAsync(int id)
    {
      return await _context.Authors.AnyAsync(e => e.Id == id);
    }
  }
}
