using CoolBooks_NinjaExperts.Data;
using CoolBooks_NinjaExperts.Models;
using CoolBooks_NinjaExperts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CoolBooks_NinjaExperts.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

            public HomeController(CoolBooks_NinjaExpertsContext context)
            {
                _context = context;
            } 



        public IActionResult Index(string orderby = "")
        {
            int bookOnPages = 8;
            var VM = new DisplayBooksViewModel();
            switch(orderby)
            {
                case "TopRated":
                    {
                        VM.Books = _context.Books
                           .Include(b => b.Authors)
                           .Include(b => b.Genres)
                           .Include(b => b.Image)
                           .OrderByDescending(b => b.Rating)
                           .ToList();
                        ViewBag.OrderBy = "TopRated";
                        break;
                    }
                case "Newest":
                    {
                        VM.Books = _context.Books
                           .Include(b => b.Authors)
                           .Include(b => b.Genres)
                           .Include(b => b.Image)
                           .OrderBy(b => b.Created)
                           .ToList();
                        ViewBag.OrderBy = "Newest";
                        break;
                    }
                case "Alphabetical":
                    {
                        VM.Books = _context.Books
                           .Include(b => b.Authors)
                           .Include(b => b.Genres)
                           .Include(b => b.Image)
                           .OrderBy(b => b.Title)
                           .ToList();
                        ViewBag.OrderBy = "Alphabetical";
                        break;
                    }
                case "":
                    {
                        VM.Books = _context.Books
                           .Include(b => b.Authors)
                           .Include(b => b.Genres)
                           .Include(b => b.Image)
                           .ToList();
                        break;
                    }
                    default:
                    break;
            }
          

            // calc the number of pages to display all the books.
            double pagecount = VM.Books.Count();
            pagecount /= bookOnPages;
            pagecount = Math.Ceiling(pagecount);
            VM.PageCount = (int)pagecount;

            Random random = new Random();
            int rndmBook = random.Next(1,VM.Books.Count());

            VM.RandomBooks = VM.Books.Where(b => b.Id == rndmBook).ToList();
            VM.CurrentPage = 0;
            VM.Books = VM.Books.Where(b => b.Id != rndmBook).Take(bookOnPages);
            return View(VM);
        }
        public IActionResult BookPages(int currentPage, int pageCount, string sortOrder) // Copy of index - will have same search functionalities
        {
            if(currentPage == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            IQueryable<Books> query = _context.Books
                           .Include(b => b.Authors)
                           .Include(b => b.Genres)
                           .Include(b => b.Image)
                           .Skip(8 * currentPage)
                           .Take(8);

            //2nd Page = 1
            switch (sortOrder)
            {
                case "TopRated":
                    {
                        query = _context.Books
                            .Include(b => b.Authors)
                            .Include(b => b.Genres)
                            .Include(b => b.Image)
                            .OrderByDescending(b => b.Rating)
                            .Skip(8 * currentPage)
                            .Take(8);
                            
                        break;
                    }
                case "Newest":
                    {
                        query = _context.Books
                            .Include(b => b.Authors)
                            .Include(b => b.Genres)
                            .Include(b => b.Image)
                            .OrderBy(b => b.Created)
                            .Skip(8 * currentPage)
                            .Take(8);
                        break;
                    }
                case "Alphabetical":
                    {
                        query = _context.Books
                           .Include(b => b.Authors)
                           .Include(b => b.Genres)
                           .Include(b => b.Image)
                           .OrderBy(b => b.Title)
                           .Skip(8 * currentPage)
                           .Take(8);
                        break;
                    }
                case "":
                    {
                        query = _context.Books
                           .Include(b => b.Authors)
                           .Include(b => b.Genres)
                           .Include(b => b.Image)
                           .Skip(8 * currentPage)
                           .Take(8);
                        break;
                    }
                default:
                    break;
            }


            var VM = new DisplayBooksViewModel();
            VM.Books = query.ToList();
            VM.PageCount = pageCount;
            VM.CurrentPage = currentPage;
            return View("Index", VM);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Termsofuse()
        {
            return View();
        }


        [Authorize (Roles = "User, Admin, Moderator")]
        public IActionResult Contact()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}