using Api.Models;
using Api.ModelsDto;

namespace Api.Services
{
    public interface IBooksService
    {
        Task<Book> AddBook(BookCreateDto bookDto);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<List<Book>> GetBooksByAuthorId(int authorId);
        Task<Book> UpdateBook(int id, BookUpdateDto bookDto);
        Task<bool> DeleteBook(int id);
    }
}