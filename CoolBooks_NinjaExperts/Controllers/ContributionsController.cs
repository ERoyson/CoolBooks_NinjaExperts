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
            var VM = new ContributionPostsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var books = _context.Books.Where(r => r.UserId == VM.UserId).ToList(); //books added by user
            VM.Books = books;

            //VM.Books = _context.Books
            //    .Include(b => b.Authors).Where(b=>b.Id == VM.BookId).ToList();
            //    .Include(b => b.Genres).Where(r => r.Reviews.Any(r => r.UserId == VM.UserId)).ToList();

            return View(VM);
        }

        public async Task<IActionResult> ReviewsAdded()
        {
            var VM = new ContributionPostsViewModel();
            VM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var reviews = _context.Reviews.Where(r => r.UserId == VM.UserId).ToList(); //all reviews posted by the user
            var books = _context.Books.Where(r => r.Reviews.Any(r => r.UserId == VM.UserId)).ToList(); //all books user posted a review on

            VM.Reviews = reviews;

            return View(VM);
        }


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






        // GET: Reviews/Edit/5
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> EditBooks(int? id)
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
            return RedirectToAction("Edit", "Books", books);
            //return View(books);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Edit(int id, int bookRating, [Bind("Id,Title,Text")] Reviews reviews)
        {
            var book = _context.Books.Where(x => x.Reviews.Any(r => r.Id == id)).FirstOrDefault();
            var updatedReview = _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
            updatedReview.Rating = bookRating;
            updatedReview.Title = reviews.Title;
            updatedReview.Text = reviews.Text;

            if (id != reviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedReview);
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
                return RedirectToAction("Details", "Books", book);
            }
            return View(reviews);
        }

        [Authorize(Roles ="Admin")]
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
        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return View("Deleted");
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }

    }
}
