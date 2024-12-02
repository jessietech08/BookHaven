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
            string cartId = HttpContext.Session.GetString("CartId");
            if (string.IsNullOrEmpty(cartId) )
            {
                return RedirectToAction("Index");
            }

            var cartItems = _context.CartItems
                .Include(ci => ci.Book)
                .Where(ci => ci.CartId == cartId)
                .ToList();

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int bookId, int quantity)
        {
            string cartId = HttpContext.Session.GetString("CartId");
            if (string.IsNullOrEmpty(cartId))
            {
                cartId = Guid.NewGuid().ToString();
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
