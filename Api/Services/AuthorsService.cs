using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly AppDbContext dbContext;

        public AuthorsService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Author> AddAuthor(AuthorCreateDto authorDto)
        {
            Author author = new Author { Name = authorDto.Name };

            dbContext.Authors.Add(author);
            await dbContext.SaveChangesAsync();

            return author;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await dbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await dbContext.Authors.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}