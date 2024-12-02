using System.ComponentModel.DataAnnotations;

namespace BookHaven.Models
{
    public class CartItemModel
    {
        /// <summary>
        /// primary key
        /// </summary>
        [Key]
        public int CartItemId { get; set; }

        /// <summary>
        /// links cart item to specific book in Book table
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// allows access to details of book
        /// </summary>
        public Book Book { get; set; }

        /// <summary>
        /// keeps track of copies of the book
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// unique identifier for user's cart
        /// </summary>
        public string CartId { get; set; } = string.Empty;
    }
}
