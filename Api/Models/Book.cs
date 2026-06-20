using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Range(1, 3000)]
        public string Year { get; set; }
        [Range(1, 99_999)]
        public int Pages { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}