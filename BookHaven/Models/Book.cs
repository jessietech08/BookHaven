#nullable disable
using System.ComponentModel.DataAnnotations;

namespace BookHaven.Models
{
    /// <summary>
    /// Book class represents a book
    /// </summary>
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public int Isbn { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public byte Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
