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



        public IActionResult Index()
        {
            int bookOnPages = 8;
            var VM = new DisplayBooksViewModel();
            VM.Books = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .ToList();

            // calc the number of pages to display all the books.
            double pagecount = VM.Books.Count();
            pagecount /= bookOnPages;
            pagecount = Math.Ceiling(pagecount);
            VM.PageCount = (int)pagecount;

            Random random = new Random();
            int rndmBook = random.Next(1,VM.Books.Count());

            VM.RandomBooks = VM.Books.Where(b => b.Id == rndmBook).ToList();

            VM.Books = VM.Books.Where(b => b.Id != rndmBook).Take(8);
            return View(VM);
        }
        public IActionResult BookPages(int page) // Copy of index - will have same search functionalities
        {
            if(page == 1)
            {
                return RedirectToAction(nameof(Index));
            }
            page -= 1; // 2nd Page = 1

            var VM = new DisplayBooksViewModel();
            VM.Books = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .ToList();
          
            VM.Books = VM.Books.Skip(8*page).Take(8);
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


        [Authorize (Roles = "User, Admin, Mod")]
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