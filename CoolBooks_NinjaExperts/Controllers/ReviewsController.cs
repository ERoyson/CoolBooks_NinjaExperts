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
    public class ReviewsController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public ReviewsController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reviews.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var VM = new BookReviewsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            VM.Book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (id == null)
            {
                return NotFound();
            }

            VM.Reviews = _context.Reviews
                .Include(r => r.User)
                //.Include(r => r.Book)
                .Where(r => r.BookId == id).ToList();
            if (VM.Reviews == null)
            {
                return NotFound();
            }

            return View(VM);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Moderator, Admin")]
        public async Task<IActionResult> Create(int bookRating, [Bind("Id,UserId,BookId,Title,Text,Rating,Created")] Reviews review)
        {
            // Books totala rating skall ändras varje gång en review skapas...
            // calc total count of bookreviews and add it to Book.Rating...

            review.Rating = bookRating;
            if (ModelState.IsValid)
            {
                double getRating = _context.Reviews.Where(r=>r.BookId == review.BookId).Select(x => x.Rating).Sum();
                getRating = getRating / (_context.Reviews.Where(r=>r.BookId==review.BookId).Count()); // +1 review to be created.
                int totalRating = (int)Math.Round(getRating);

                Books books = _context.Books.Where(x=>x.Id == review.BookId).FirstOrDefault();
                books.Rating = totalRating;
                _context.Books.Update(books);
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Books", books); //Skickar användaren till samma sida efter inskickad review
            }
            return View(review); //Ändra till annan sida ifall reviewn misslyckas.
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }
            return View(reviews);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,Rating,Created,Deleted")] Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.Id))
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
            return View(reviews);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
