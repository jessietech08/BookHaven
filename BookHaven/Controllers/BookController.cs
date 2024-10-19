using BookHaven.Data;
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create

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
