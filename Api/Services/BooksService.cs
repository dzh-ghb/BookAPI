using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly AppDbContext dbContext;

        public BooksService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Book> AddBook(BookCreateDto bookDto)
        {
            var authorFromDb = await dbContext.Authors.FirstOrDefaultAsync(i => i.Id == bookDto.AuthorId);

            if (authorFromDb == null)
            {
                return null;
            }

            Book book = new Book
            {
                Title = bookDto.Title,
                Year = bookDto.Year,
                Pages = bookDto.Pages,
                AuthorId = bookDto.AuthorId
            };

            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            return book;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            var bookFromDb = await dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);

            return bookFromDb != null ? bookFromDb : null;
        }

        public async Task<List<Book>> GetBooksByAuthorId(int authorId)
        {
            var booksByAuthor = await dbContext.Books.Where(i => i.AuthorId == authorId).ToListAsync();

            return (booksByAuthor != null && booksByAuthor.Count > 0) ? booksByAuthor : null;
        }

        public async Task<Book> UpdateBook(int id, BookUpdateDto bookDto)
        {
            var bookFromDb = await dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);

            if (bookFromDb == null)
            {
                return null;
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

            return bookFromDb;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var bookFromDb = await dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);

            if (bookFromDb == null)
            {
                return false;
            }

            dbContext.Books.Remove(bookFromDb);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}