using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AuthorsController : BaseController
    {
        private readonly AppDbContext dbContext;

        public AuthorsController(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto authorDto)
        {
            Author author = new Author { Name = authorDto.Name };

            dbContext.Authors.Add(author);
            await dbContext.SaveChangesAsync();

            return Ok(await GetById(author.Id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await dbContext.Authors.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authorFromDb = await dbContext.Authors.FirstOrDefaultAsync(i => i.Id == id);

            if (authorFromDb == null)
            {
                return NotFound("Пользователь с таким ID не найден");
            }

            return Ok(authorFromDb);
        }
    }
}