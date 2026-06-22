using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class BooksController : BaseController
    {
        private readonly AppDbContext dbContext;

        public BooksController(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateDto bookDto)
        {
            Book book = new Book
            {
                Title = bookDto.Title,
                Year = bookDto.Year,
                Pages = bookDto.Pages,
                AuthorId = bookDto.AuthorId
            };

            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            return Ok(await GetById(book.Id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await dbContext.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookFromDb = await dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);

            if (bookFromDb == null)
            {
                return NotFound("Книги с таким ID не найдено");
            }

            return Ok(bookFromDb);
        }

        [HttpGet("{authorId}")]
        public async Task<IActionResult> GetByAuthorId(int authorId)
        {
            var booksByAuthor = await dbContext.Books.Where(i => i.AuthorId == authorId).ToListAsync();

            if (booksByAuthor == null || booksByAuthor.Count == 0)
            {
                return NotFound("Книг указанного автора не найдено");
            }

            return Ok(booksByAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto bookDto)
        {
            var bookFromDb = await dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);

            if (bookFromDb == null)
            {
                return NotFound("Книги с таким ID не найдено");
            }

            if (!string.IsNullOrWhiteSpace(bookDto.Title))
            {
                bookFromDb.Title = bookDto.Title;
            }
            if (bookDto.Year > 0 && bookDto.Year < 3000)
            {
                bookFromDb.Year = bookDto.Year;
            }
            if (bookDto.Pages > 0 && bookDto.Pages < 99_999)
            {
                bookFromDb.Pages = bookDto.Pages;
            }

            await dbContext.SaveChangesAsync();

            return Ok(await GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);

            if (bookFromDb == null)
            {
                return NotFound("Книги с таким ID не найдено");
            }

            dbContext.Books.Remove(bookFromDb);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}