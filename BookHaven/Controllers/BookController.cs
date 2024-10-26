using BookHaven.Data;
using BookHaven.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookHaven.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public BookController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) 
        { 
            _context = context;
            WebHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            string stringFileName = UploadFile(model);
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Isbn = model.Isbn,
                    Title = model.Title,
                    Author = model.Author,
                    Genre = model.Genre,
                    Image = stringFileName,
                    Description = model.Description,
                    Price = model.Price
                };

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // https://www.youtube.com/watch?v=7nFErqpxtbg
        private string UploadFile(CreateBookViewModel model)
        {
            string fileName = null;
            if (model.Image != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "uploads");
                fileName = Guid.NewGuid().ToString() + "-" + model.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return fileName;
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
