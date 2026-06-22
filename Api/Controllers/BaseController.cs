using Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public BaseController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}