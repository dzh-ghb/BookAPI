using System.ComponentModel.DataAnnotations;

namespace Api.ModelsDto
{
    public class AuthorCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}