#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class BooksController : Controller //Controller start
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public BooksController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        } //Controller end

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
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
            Books books = new Books();
            books.Genres = _context.Genres.ToList();
            
            return View(books);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<string> Genres, List<string> sAuthors, [Bind("Id,Title,Description,Published,ISBN")] Books book)
        {
            // var nameCookie = Request.Cookies[".AspNetCore.Antiforgery.dskp_3aZLLY"];
            
            

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
            return View(book);
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
