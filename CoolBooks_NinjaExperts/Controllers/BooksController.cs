#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Data;

using CoolBooks_NinjaExperts.ViewModels;

namespace CoolBooks_NinjaExperts.Models
{
    public class BooksController : Controller //Controller start
    {

        private readonly CoolBooks_NinjaExpertsContext _context;

        public BooksController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        public ActionResult Index(string sortOrder, string searchString)
        {
            var VM = new CreateBookViewModel();
            VM.Books = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .ToList();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.SerieSort = sortOrder == "Serie" ? "Serie_desc" : "Serie";
            ViewBag.AuthorSort = sortOrder == "Author" ? "Author_desc" : "Author";
            ViewBag.GenreSort = sortOrder == "Genre" ? "Genre_desc" : "Genre";
            ViewBag.RatingSort = sortOrder == "Rating" ? "Rating_desc" : "Rating";
            ViewBag.CreatedSort = sortOrder == "Created" ? "Created_desc" : "Created";

            bool bookSearch = true;
            if (!String.IsNullOrEmpty(searchString))
            {

                VM.Books = VM.Books.Where(s => s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) || s.BookSeries.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                if(!VM.Books.Any())
                    bookSearch = false;
                    

            }
            if (!string.IsNullOrEmpty(searchString) && bookSearch == false)
            {
                var authorResult = new Authors();

                authorResult = _context.Books
                    .SelectMany(b => b.Authors)
                    .FirstOrDefault(a => a.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                
                VM.Books = authorResult.Books;
            }
            
            
            switch (sortOrder)
            {
                case "title_desc":

                    VM.Books = VM.Books.OrderByDescending(b => b.Title);
                    break;
                case "Serie":
                    VM.Books = VM.Books.OrderBy(b => b.BookSeries);
                    break;
                case "Serie_desc":
                    VM.Books = VM.Books.OrderByDescending(b => b.BookSeries);

                    break;
                //case "Author":

                //    VM.Books = VM.Books.OrderBy(a => a.FullName);
                //    break;
                //case "Author_desc":
                //    VM.Books = VM.Books.OrderByDescending(a => a.FullName);
                //    break;
                case "Rating":
                    VM.Books = VM.Books.OrderBy(r => r.Rating);
                    break;
                case "Rating_desc":
                    VM.Books = VM.Books.OrderByDescending(r => r.Rating);
                    break;
                case "Created":
                    VM.Books = VM.Books.OrderBy(c => c.Created);
                    break;
                case "Created_desc":
                    VM.Books = VM.Books.OrderByDescending(c => c.Created);
                    break;
                default:
                    VM.Books = VM.Books.OrderBy(b => b.Title);

                    break;
            }
            return View(VM);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {   
            var book = new CreateBookViewModel();
            var getDbGenres = _context.Genres.ToList();

            foreach (var item in getDbGenres)
            {
                var listGenres = new SelectGenresViewModel();
                listGenres.Genres = item;
                listGenres.IsSelected = false;
                book.ListGenres.Add(listGenres);
            }
           
            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<string> Authors, CreateBookViewModel FormBook)
        {
            var book = FormBook.Book;
            

            // Add-Genres to book
            foreach(var genre in FormBook.ListGenres)
            {
                if(genre.IsSelected)
                {
                    var bookGenre = new Genres { Id = genre.Genres.Id };
                    _context.Genres.Attach(bookGenre);
                    book.Genres.Add(bookGenre);
                }
            }

            // Add-Authors to book
            foreach (var authors in Authors)
            {
                var author = _context.Authors.FirstOrDefault(a => a.FullName == authors);
                if(author == null) // Add New Author
                {
                    var newAuthor = new Authors();
                    newAuthor.FullName = authors;
                    newAuthor.Biography = "Needs to be added...";
                    book.Authors.Add(newAuthor);
                }
                else // Add Existing Author.
                {
                    _context.Authors.Attach(author);
                    book.Authors.Add(author);
                }
                
            }

            // Image handeling
            foreach (var file in Request.Form.Files)
            {
                Images img = new Images();
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                img.Image = ms.ToArray();
                ms.Close();
                ms.Dispose();

                img.Thumbnail = CreateThumbnail(img.Image);

                book.Image = img;
            }


            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(FormBook);
        }

        // Covert to Thumbnail
        public byte[] CreateThumbnail(byte[] imgFile)
        {

            MemoryStream ms = new MemoryStream(imgFile);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            // convert img to thumbnail
            var thumbimg = image.GetThumbnailImage(64, 64, new System.Drawing.Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);


            // convert to byte[]
            using (var ms2 = new MemoryStream())
            {
                thumbimg.Save(ms2, image.RawFormat);
                return ms2.ToArray();
            }
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ISBN,Created,Deleted")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(books);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Books.FindAsync(id);
            _context.Books.Remove(books);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
