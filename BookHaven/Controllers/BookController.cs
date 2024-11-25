using BookHaven.Data;
using BookHaven.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            Book? bookToEdit = await _context.Books.FindAsync(id);

            if (bookToEdit == null)
            {
                return NotFound();
            }
            return View(bookToEdit);
        }

        // POST: Edit
        [HttpPost, ActionName("Edit")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book bookToEdit = await _context.Books.FindAsync(id);
                if (bookToEdit != null)
                {
                    if (model.Image != null)
                    {
                        string stringFileName = UploadFile(model);
                        bookToEdit.Image = stringFileName;
                    }

                    bookToEdit.Isbn = model.Isbn;
                    bookToEdit.Title = model.Title;
                    bookToEdit.Author = model.Author; 
                    bookToEdit.Genre = model.Genre;
                    bookToEdit.Description = model.Description;
                    bookToEdit.Price = model.Price;

                    _context.Books.Update(bookToEdit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // GET: Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Book booktToDelete = await _context.Books.FindAsync(id);

            if (booktToDelete != null)
            {
                _context.Books.Remove(booktToDelete);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Details
        public async Task<ActionResult> Details(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}
