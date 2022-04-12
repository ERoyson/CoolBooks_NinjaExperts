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
        } //Controller end

        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSort = sortOrder == "Author" ? "Author_desc" : "Author";
            var books = from b in _context.Books
                         select b;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
          
                default:
                    books = books.OrderBy(b =>b.Title);
                    break;
            }
            return View(books.ToList());
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
