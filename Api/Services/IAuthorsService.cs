using Api.Models;
using Api.ModelsDto;

namespace Api.Services
{
    public interface IAuthorsService
    {
        Task<Author> AddAuthor(AuthorCreateDto authorDto);
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
    }
}