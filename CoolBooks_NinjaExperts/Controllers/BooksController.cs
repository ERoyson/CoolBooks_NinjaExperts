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
                .Where(b=>b.Deleted == null)
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
                //break;
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
             .Where(b => b.Deleted == null)
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
            VM.UserId = userId;

            VM.Lists = _context.Lists.Where(l => l.UserId == userId).ToList();

            VM.FlaggedReviews = _context.FlaggedReviews
                .Include(x => x.Review)
                .ThenInclude(x => x.Comments)
                .Include(x => x.Flagged)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToList();


            if (id == null)
            {
                return NotFound();
            }

            VM.FlaggedComments = _context.FlaggedComments
                .Include(x => x.Comments)
                .Include(x => x.Flagged)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToList();
            if (id == null)
            {
                return NotFound();
            }



            VM.Comments = _context.Comments
               .Include(r => r.Replies)
               .ThenInclude(c => c.User)
               .Include(c => c.Reviews)
               .ThenInclude(c => c.Book)
               .Include(r => r.User)
               .Include(r => r.CommentLikes)
               .Include(r => r.CommentDislikes)
               .Where(c => c.IsBlocked == null || false)
               .OrderByDescending(r => r.Created).ToList();

            VM.Comments = _context.Comments
               .Include(r => r.Replies)
               .ThenInclude(c => c.User)
               .Include(c => c.Reviews)
               .ThenInclude(c => c.Book)
               .Include(r => r.User)
               .Include(r => r.CommentLikes)
               .Include(r => r.CommentDislikes)
               .Where(c => c.IsBlocked == null || false)
               .OrderByDescending(r => r.Created).ToList();

            //Lägg till fler filtreringsalternativ på reviews, ex. högst poäng, flest gillade review etc.
            VM.Reviews = _context.Reviews
                    .Include(r => r.Comments.Where(c => c.IsBlocked == null || false))
                        .ThenInclude(c => c.CommentLikes)
                    .Include(r => r.Comments.Where(c => c.IsBlocked == null || false))
                        .ThenInclude(c => c.CommentDislikes)
                    .Include(c => c.Comments.Where(c => c.IsBlocked == null || false))
                        .ThenInclude(c => c.User)
                    .Include(r => r.Comments.Where(c => c.IsBlocked == null || false))
                        .ThenInclude(r => r.Replies)
                    .Include(r => r.User)
                    .Include(r => r.Book)
                    .Include(r => r.ReviewLikes)
                    .Include(r => r.ReviewDislikes)
                    .Where(r => r.BookId == id && r.IsBlocked == null || false)
                    
                    .OrderByDescending(r => r.Created).ToList();


            VM.Book = _context.Books
                .Where(b => b.Deleted == null)
                .Include(x => x.Image)
                .Include(x => x.Authors)
                .Include(x => x.Genres)
                .FirstOrDefault(x => x.Id == id);

            if (VM.Reviews == null)
            {
                return NotFound();
            }

            return View(VM);
        }


        // GET: Books/Create
        [Authorize(Roles = "Admin, User")]
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

      [HttpPost]
      [ValidateAntiForgeryToken]
      [Authorize(Roles = "Admin, User")]
      public async Task<IActionResult> Create(List<string> Authors, CreateBookViewModel FormBook)
      {
         var book = FormBook.Book;
         book.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
                newAuthor.Image = _context.Images.FirstOrDefault(i => i.Id == 26); // Dummy image for authors -> needs to be added by admin
                book.Authors.Add(newAuthor);
            }
            else // Add Existing Author.
            {
                _context.Authors.Attach(author);
                book.Authors.Add(author);
            }

         }

        // Image handeling
         if (Request.Form.Files.Count() == 0) //If no image is provided by the user
         {
            Images img = new Images();
            MemoryStream ms = new MemoryStream();
            img = _context.Images.Where(a => a.Id == 26).FirstOrDefault(); //Id = 26 är defaultbilden
            book.Image = img;
         }
         else
         {
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
         var thumbimg = image.GetThumbnailImage(128, 200, new System.Drawing.Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);


         // convert to byte[]
         using (var ms2 = new MemoryStream())
         {
            thumbimg.Save(ms2, image.RawFormat);
            return ms2.ToArray();
         }
      }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin, Moderator, User")]
        public async Task<IActionResult> Edit(int? id)
        {
            var VM = new ContributionsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = _context.Books.Include(b=>b.Authors).Include(b=>b.Genres).Include(b=>b.Image).Where(b => b.Id == id).FirstOrDefault();

            if (VM.UserId == book.UserId || User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }

            return NotFound();
        }

      // POST: Books/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ISBN,Published,Created,BookSeries,UserId")] Books book, List<string> Authors, List<string> Genres)
      {
            var oldBook = _context.Books
                .Include(b=>b.Authors)
                .Include(b=>b.Genres)
                .Include(b=>b.Image)
                .Include(b=>b.Reviews)
                .FirstOrDefault(b=>b.Id == book.Id);
            // --------------------------------------------------------------------------------- Authors
            var removeAuthor = new List<Authors>();
            var Authorsbooks = new List<AuthorsBooks>();
            for(int i = 0; i<oldBook.Authors.Count(); i++)
            {
                removeAuthor.Add(oldBook.Authors.FirstOrDefault(a => a.FullName != Authors[i] || Authors[i] == null));
            }
            if(removeAuthor.Count > 0)
            {
                if (removeAuthor[0] != null)
                {
                    foreach (var author in removeAuthor)
                    {
                        var authorbook = _context.AuthorsBooks.FirstOrDefault(a => a.AuthorsId == author.Id && a.BooksId == oldBook.Id);
                        Authorsbooks.Add(authorbook);
                    }
                    foreach (var author in Authorsbooks)
                    {
                        _context.AuthorsBooks.Remove(author);
                    }
                    _context.SaveChanges();
                }
            }
          
            foreach (var authors in Authors)
            {
                var author = _context.Authors.FirstOrDefault(a => a.FullName == authors);

                if (oldBook.Authors.Any(a => a.FullName == author.FullName))
                {
                    book.Authors.Add(oldBook.Authors.Where(a => a.FullName == authors).FirstOrDefault());
                    continue;
                }
                else if (author == null) // Add New Author
                {
                    if (authors != null) // if we want to delete an author from book (in edit books)
                    {
                        var newAuthor = new Authors();

                        newAuthor.FullName = authors;
                        newAuthor.Biography = "Needs to be added...";
                        book.Authors.Add(newAuthor);
                    }
                    
                }
                else // Add Existing Author.
                {
                    _context.Authors.Attach(author);
                    book.Authors.Add(author);
                }
            }

            // --------------------------------------------------------------------------------- Genres
            var removeGenres = new List<Genres>();
            var genresBooks = new List<BooksGenres>();
            for (int i = 0; i < oldBook.Genres.Count(); i++)
            {
                removeGenres.Add(oldBook.Genres.FirstOrDefault(a => a.Name != Genres[i] || Genres[i] == null));
            }
            if(removeGenres.Count > 0)
            {
                if (removeGenres[0] != null)
                {
                    foreach (var genres in removeGenres)
                    {
                        var bookgenre = _context.BooksGenres.FirstOrDefault(a => a.GenresId == genres.Id && a.BooksId == oldBook.Id);
                        genresBooks.Add(bookgenre);
                    }
                    foreach (var genre in genresBooks)
                    {
                        _context.BooksGenres.Remove(genre);
                    }
                    _context.SaveChanges();
                }
            }
            foreach (var genres in Genres)
            {
                var genre = _context.Genres.FirstOrDefault(g => g.Name == genres);

                if (oldBook.Genres.Any(g => g.Name == genre.Name))
                {
                    book.Genres.Add(oldBook.Genres.Where(a => a.Name == genres).FirstOrDefault());
                    continue;
                }
                else if (genre == null) // Ignore new Genres
                {
                    continue;
                }
                else // Add Existing Genre.
                {
                    _context.Genres.Attach(genre);
                    book.Genres.Add(genre);
                }
            }

            oldBook.Title= book.Title;
            oldBook.ISBN = book.ISBN;
            if(book.Published!=null) {oldBook.Published = book.Published;}
            oldBook.BookSeries = book.BookSeries;
            oldBook.Description= book.Description;
            oldBook.Authors = book.Authors;
            oldBook.Genres = book.Genres;

            // Image handeling
            
            if (Request.Form.Files.Count() != 0) //If no image is provided by the user
            {
                foreach (var file in Request.Form.Files)
                {
                    Images img = new Images();
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    img.Image = ms.ToArray();
                    ms.Close();
                    ms.Dispose();

                    img.Thumbnail = CreateThumbnail(img.Image);

                    oldBook.Image = img;
                }
            }


            if (id != book.Id)
            {
            return NotFound();
            }
            
            
         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(oldBook);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!BooksExists(book.Id))
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
         return View(book);
      }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(int? id)
        {
            var VM = new ContributionsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = _context.Books.Where(r => r.Id == id).FirstOrDefault();

            if (VM.UserId == book.UserId || User.IsInRole("Admin"))
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
            return NotFound();
        }

      // POST: Books/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
             var books = await _context.Books.FindAsync(id);
            books.Deleted = DateTime.Now;
             _context.Books.Update(books);
            await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
      }

      private bool BooksExists(int id)
      {
         return _context.Books.Any(e => e.Id == id);
      }
   }
}
