using BookHaven.Data;
using BookHaven.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context) 
        { 
            _context = context;
        }

        // GET: BookController
        public async Task<ActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(Book b)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Isbn = b.Isbn,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    Image = b.Image,
                    Description = b.Description,
                    Price = b.Price
                };

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(b);
        }

        // GET: Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Edit

        // GET: Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Delete

        // GET: Details
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
