using Api.ModelsDto;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBooksService service;

        public BooksController(IBooksService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateDto bookDto)
        {
            var book = await service.AddBook(bookDto);

            // await GetById(book.Id)
            return book != null ? Ok(book) : BadRequest("Автор не найден");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllBooks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookFromDb = await service.GetBookById(id);

            return bookFromDb != null ? Ok(bookFromDb) : NotFound("Книги с таким ID не найдено");
        }

        [HttpGet("{authorId}")]
        public async Task<IActionResult> GetByAuthorId(int authorId)
        {
            var booksByAuthor = await service.GetBooksByAuthorId(authorId);

            return (booksByAuthor != null && booksByAuthor.Count > 0) ? Ok(booksByAuthor) : NotFound("Книг указанного автора не найдено");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto bookDto)
        {
            var bookFromDb = await service.UpdateBook(id, bookDto);

            return bookFromDb != null ? Ok(bookFromDb) : NotFound("Книги с таким ID не найдено");
            // return Ok(await GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await service.DeleteBook(id);

            return bookFromDb ? NoContent() : NotFound("Книги с таким ID не найдено");
        }
    }
}