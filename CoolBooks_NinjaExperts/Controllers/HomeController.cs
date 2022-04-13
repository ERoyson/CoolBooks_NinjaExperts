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
            var book = new List<Books>();
            book = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .Take(8)
                .ToList();

            return View(book);
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