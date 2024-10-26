using System.ComponentModel.DataAnnotations;

namespace BookHaven.Models
{
    public class CreateBookViewModel
    {
        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string Isbn { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Genre { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        public IFormFile Image { get; set; } 

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
