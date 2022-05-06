#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Data;
using CoolBooks_NinjaExperts.Models;
using CoolBooks_NinjaExperts.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CoolBooks_NinjaExperts.Controllers
{
    public class ContributionsController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public ContributionsController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> BooksAdded()
        {
            var VM = new ContributionsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var books = _context.Books.Where(r => r.UserId == VM.UserId).ToList(); //books added by user
            VM.Books = books;

            return View(VM);
        }

        public async Task<IActionResult> ReviewsAdded()
        {
            var VM = new ContributionsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var reviews = _context.Reviews.Where(r => r.UserId == VM.UserId).ToList(); //all reviews posted by the user
            var books = _context.Books.Where(r => r.Reviews.Any(r => r.UserId == VM.UserId)).ToList(); //all books user posted a review on

            VM.Reviews = reviews;

            return View(VM);
        }

        public async Task<IActionResult> MyLists() //Displays index on Contributions/MyLists
        {
            var VM = new ContributionsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var books = _context.Lists.Include(b => b.Books).ToList();
            var lists = _context.Lists.Where(r => r.UserId == VM.UserId).ToList();
            VM.Lists = lists;

            return View(VM);
        }

        public async Task<IActionResult> QuizAdded()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var quiz = _context.Quiz.Include(x => x.Book).Include(y => y.User).Where(r => r.UserId == userId).ToList();
            ViewBag.UserId = userId;
            return View(quiz);
        }

    }
}
