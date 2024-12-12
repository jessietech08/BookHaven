using BookHaven.Data;
using BookHaven.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public CartController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieves the CartId stored in the user's session
            string cartId = HttpContext.Session.GetString("CartId");
            if (string.IsNullOrEmpty(cartId) )
            {
                return RedirectToAction("Index");
            }

            var cartItems = _context.CartItems
                .Include(cartItem => cartItem.Book)
                .Where(cartItem => cartItem.CartId == cartId)
                .ToList();

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int bookId, int quantity)
        {
            // Retrieves the CartId stored in the user's session
            string cartId = HttpContext.Session.GetString("CartId");
            if (string.IsNullOrEmpty(cartId))
            {
                // Creates a new unique CartId using a GUID
                // (Globally Unique Identifier)
                cartId = Guid.NewGuid().ToString();
                // Saves the generated CartId into the session, associating it with the user
                HttpContext.Session.SetString("CartId", cartId);
            }

            var cartItem = new CartItemModel
            {
                CartId = cartId,
                BookId = bookId,
                Quantity = quantity,
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
