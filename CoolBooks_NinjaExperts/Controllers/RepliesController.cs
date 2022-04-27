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

namespace CoolBooks_NinjaExperts.Controllers
{
    public class RepliesController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public RepliesController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        // GET: Replies
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Replies.ToListAsync());

            var coolBooks_NinjaExpertsContext = _context.Replies.Include(c => c.User);
            return View(await coolBooks_NinjaExpertsContext.ToListAsync());
        }

        // GET: Replies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replies = await _context.Replies
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (replies == null)
            {
                return NotFound();
            }

            return View(replies);
        }

        // GET: RepliesController/Create
        public PartialViewResult Create(string comment_and_review)
        {
            string[] temp = comment_and_review.Split(',');
            string comment = temp[0];
            string review = temp[1];

            int reviewId = int.Parse(review);
            int commentId = int.Parse(comment);

            var VM = new BookReviewsViewModel(); //ViewModel-object
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            VM.UserId = userId;
            VM.CommentId = commentId;
            VM.ReviewId = review;

            VM.Review = _context.Reviews.Where(r => r.Id == reviewId && r.Comments.Any(c=>c.Id == commentId)) 
                .FirstOrDefault();

            return PartialView("_ReplyForm", VM);
        }

        // POST: RepliesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int commentId, string userId, [Bind("Id,Reply")] Replies reply)
        {
            var VM = new BookReviewsViewModel();
            VM.User = _context.UserInfo.FirstOrDefault(u => u.Id == userId);
            reply.UserId = userId;
            VM.Comment = _context.Comments.FirstOrDefault(r => r.Id == commentId);
            VM.Comment.Replies.Add(reply);

            VM.Book = _context.Books.Where(b => b.Reviews.Any(r => r.Comments.Any(c=>c.Id == commentId))).FirstOrDefault();

            if (ModelState.IsValid)
            {
                _context.Update(VM.Comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Books", VM.Book);
            }
            return View(reply);
        }


        // GET: RepliesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replies = await _context.Replies.FindAsync(id);
            if (replies == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", replies.UserId);
            return View(replies);
        }


        // POST: Replies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Created,Deleted,IsFlagged,IsBlocked")] Replies replies)
        {
            if (id != replies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(replies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepliesExists(replies.Id))
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
            ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", replies.UserId);
            return View(replies);
        }

        // GET: RepliesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replies = await _context.Replies
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (replies == null)
            {
                return NotFound();
            }

            return View(replies);
        }

        // POST: RepliesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var replies = await _context.Replies.FindAsync(id);
            _context.Replies.Remove(replies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepliesExists(int id)
        {
            return _context.Replies.Any(e => e.Id == id);
        }
    }
}
