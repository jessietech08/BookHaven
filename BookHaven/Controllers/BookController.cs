using BookHaven.Data;
using BookHaven.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Security.Policy;
using static NuGet.Packaging.PackagingConstants;

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
        /// <summary>
        /// This method uploads the image to the server's "uploads" folder and returns the unique file name that was used
        /// </summary>
        /// <param name="model">Uses CreateBookViewModel</param>
        /// <returns>unique file name</returns>
        private string UploadFile(CreateBookViewModel model)
        {
            string fileName = null;
            if (model.Image != null)
            {
                // Combines the root path of web application with the "uploads" folder
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "uploads");
                // Creates a unique file name using a GUID to avoid overwriting existing files
                fileName = Guid.NewGuid().ToString() + "-" + model.Image.FileName;
                // Creates a new file stream at the specified path.
                string filePath = Path.Combine(uploadDir, fileName);
                // Copies the uploaded file’s content to the new file.
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
        public async Task<IActionResult> Delete(int id)
        {
            Book? bookToDelete = await _context.Books.FindAsync(id);

            if (bookToDelete == null)
            {
                return NotFound();
            }

            return View(bookToDelete);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Book booktToDelete = await _context.Books.FindAsync(id);

            if (booktToDelete != null)
            {
                _context.Books.Remove(booktToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = booktToDelete.Title + " was deleted";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Details
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
