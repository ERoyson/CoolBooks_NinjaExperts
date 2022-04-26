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
    
    public class CommentsController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public CommentsController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        // GET: CommentsController1
        public async Task<IActionResult> Index()
        {
            var coolBooks_NinjaExpertsContext = _context.Comments.Include(c => c.User);
            return View(await coolBooks_NinjaExpertsContext.ToListAsync());
        }

        // GET: CommentsController1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        //[Authorize(Roles = "User, Moderator, Admin")]
        public PartialViewResult Create(string review)
        {
            int reviewId = int.Parse(review);
            var VM = new BookReviewsViewModel(); //ViewModel-object
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            VM.UserId = userId;
            VM.Review = _context.Reviews.Where(r => r.Id == reviewId)
                .FirstOrDefault();
            //ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id");
            return PartialView("_CommentForm", VM);
        }

        //Hämta reviewlista
        //Ladda nuvarande review med en kommentar


        // POST: CommentsController1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Moderator, Admin")]
        public async Task<IActionResult> Create(int reviewId, string userId, [Bind("Id,Comment")] Comments comment)
        {
            var VM = new BookReviewsViewModel();
            VM.User = _context.UserInfo.FirstOrDefault(u => u.Id == userId);
            comment.UserId = userId;
            VM.Review = _context.Reviews.FirstOrDefault(r => r.Id == reviewId);
            VM.Review.Comments.Add(comment);

            VM.Book = _context.Books.Where(b => b.Reviews.Any(r => r.Id == reviewId)).FirstOrDefault();

            if (ModelState.IsValid)
            {
                _context.Update(VM.Review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","Books", VM.Book);
            }
            return View(comment);
        }

        // GET: CommentsController1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", comments.UserId);
            return View(comments);
        }

        // POST: CommentsController1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ReviewId,Comment,Created,Deleted,IsFlagged,IsBlocked")] Comments comments)
        {
            if (id != comments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentsExists(comments.Id))
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
            ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", comments.UserId);
            return View(comments);
        }

        // GET: CommentsController1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // POST: CommentsController1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comments = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }

        //public ActionResult LoadPartialView(BookReviewsViewModel VM)
        //{
        //    return PartialView("_CommentForm", VM);
        //}
    }
}
