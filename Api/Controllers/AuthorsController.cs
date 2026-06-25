using Api.ModelsDto;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AuthorsController : BaseController
    {
        private readonly IAuthorsService service;

        public AuthorsController(IAuthorsService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto authorDto)
        {
            return Ok(await service.AddAuthor(authorDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllAuthors());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authorFromDb = await service.GetAuthorById(id);

            return authorFromDb != null ? Ok(authorFromDb) : NotFound("Пользователь с таким ID не найден");
        }
    }
}