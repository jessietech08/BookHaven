#nullable disable
using System.ComponentModel.DataAnnotations;

namespace BookHaven.Models
{
    /// <summary>
    /// Book class represents a book
    /// </summary>
    public class Book
    {
        [Key]
        public int BookId { get; set; }

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
        public byte[] Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
