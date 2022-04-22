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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoolBooks_NinjaExperts.Models
{

   //[Authorize(Roles = "Admin, Moderator, User")]
   public class BooksController : Controller //Controller start
   {
      private readonly CoolBooks_NinjaExpertsContext _context;

      public BooksController(CoolBooks_NinjaExpertsContext context)
      {
         _context = context;
      }

      // [Authorize(Roles = "User, Admin, Mod")]
      public IActionResult Index(string sortOrder, string searchString, string currentFilter)
      {

            int bookOnPage = 10;
            var VM = new DisplayBooksViewModel();

            VM.Books = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .ThenInclude(g => g.Books)
                .ThenInclude(bg => bg.Genres)
                .Include(b => b.Image)
                .ToList();

         ViewBag.CurrentSort = sortOrder;
         ViewBag.TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
         ViewBag.SerieSort = sortOrder == "Serie" ? "Serie_desc" : "Serie";
         ViewBag.AuthorSort = sortOrder == "Author" ? "Author_desc" : "Author";
         ViewBag.GenreSort = sortOrder == "Genre" ? "Genre_desc" : "Genre";
         ViewBag.RatingSort = sortOrder == "Rating" ? "Rating_desc" : "Rating";
         ViewBag.CreatedSort = sortOrder == "Created" ? "Created_desc" : "Created";

            if(searchString == null)
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
            VM.Books =
                VM.Books.Where(s => s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                s.BookSeries.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                s.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                s.Authors.Any(a => a.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                s.Genres.Any(a => a.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }


            double pagecount = VM.Books.Count();
            pagecount /= bookOnPage;
            pagecount = Math.Ceiling(pagecount);
            VM.PageCount = (int)pagecount;
            VM.CurrentPage = 0;
            VM.Books = VM.Books.Take(10);

            switch (sortOrder)
         {
            case "title_desc":
               VM.Books = VM.Books.OrderByDescending(b => b.Title);
               break;
            case "Author":
               VM.Books = VM.Books.OrderBy(b => b.BookSeries);
               break;
            case "Author_desc":
               VM.Books = VM.Books.OrderByDescending(b => b.BookSeries);
               break;
            case "Rating":
               VM.Books = VM.Books.OrderBy(r => r.Rating);
               break;
            case "Rating_desc":
               VM.Books = VM.Books.OrderByDescending(r => r.Rating);
               break;
            //case "Genre":
            //    VM.Books = VM.Books.OrderBy(c => c.Genres);
            //    break;
            //case "Genre_desc":
            //    VM.Books = VM.Books.OrderByDescending(c => c.Genres);
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

      public IActionResult Bookpages(int currentPage, int pageCount)
      {
         if (currentPage == 0)
         {
            return RedirectToAction(nameof(Index));
         }

         var query = _context.Books       
             .Include(b => b.Authors)     
             .Include(b => b.Genres)
             .Include(b => b.Image)
             .Skip(10 * currentPage)   
             .Take(10);                             

         var VM = new DisplayBooksViewModel();     
         VM.Books = query.ToList();       
         VM.PageCount = pageCount;        
         VM.CurrentPage = currentPage;    
         return View("Index", VM);        
      }

      // GET: Books/Details/5
      //[Authorize(Roles = "Admin, Moderator, User")]
      public async Task<IActionResult> Details(int? id)
      {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            var VM = new BookReviewsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the logged in user's userId
            VM.FlaggedReviews = _context.FlaggedReviews
                .Include(x=>x.Review)
                .Include(x=>x.Flagged)
                .Include(x=>x.User)
                .Where(x => x.UserId == userId)
                .ToList();

            if (id == null)
            {
                return NotFound();
            }

            //Lägg till fler filtreringsalternativ på reviews, ex. högst poäng, flest gillade review etc.
            VM.Reviews = _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .Where(r => r.BookId == id && r.IsBlocked == null || false) 
                .OrderByDescending(r=>r.Created).ToList();

            VM.Book = _context.Books
                .Include(x=>x.Image)
                .Include(x=>x.Authors)
                .Include(x=>x.Genres)
                .FirstOrDefault(x => x.Id == id);

            if (VM.Reviews == null)
            {
            return NotFound();
            }

            return View(VM);
      }

      // GET: Books/Create
      [Authorize(Roles = "Admin")]
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
         foreach (var genre in FormBook.ListGenres)
         {
            if(genre.IsSelected == true)
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
            if (author == null) // Add New Author
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
      [Authorize(Roles = "Admin")]
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
      [Authorize(Roles = "Admin, Moderator")]
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
      [Authorize(Roles = "Admin")]
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
