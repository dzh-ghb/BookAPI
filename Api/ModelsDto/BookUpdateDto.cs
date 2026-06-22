using System.ComponentModel.DataAnnotations;

namespace Api.ModelsDto
{
    public class BookUpdateDto
    {
        [Required]
        public string Title { get; set; }
        [Range(1, 10_000)]
        public int Year { get; set; }
        [Range(1, 100_000)]
        public int Pages { get; set; }
    }
}