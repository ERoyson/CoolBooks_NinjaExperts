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
    public class RepliesController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public RepliesController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Replies.ToListAsync());

            var coolBooks_NinjaExpertsContext = _context.Replies.Include(c => c.User);
            return View(await coolBooks_NinjaExpertsContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")] 
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


        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replies = await _context.Replies.FindAsync(id);
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (user == replies.UserId || User.IsInRole("Admin"))
            {
                if (replies == null)
                {
                    return NotFound();
                }
                ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", replies.UserId);
                return View(replies);
            }

            return NotFound();
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reply")] Replies newReply)
        {
            var book = _context.Books.Where(x => x.Reviews.Any(y => y.Comments.Any(z => z.Replies.Any(r => r.Id == id)))).FirstOrDefault();
            var updatedReply = _context.Replies.Where(r => r.Id == id).FirstOrDefault();
            updatedReply.Reply = newReply.Reply;


            //var reply = await _context.Replies.FindAsync(newReply.Id);
            //reply.Reply = newReply.Reply;

            if (id != newReply.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedReply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepliesExists(newReply.Id))
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
            //ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", reply.UserId);
            return View(newReply);
        }

        [Authorize(Roles = "Admin")]
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

        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var replies = await _context.Replies.FindAsync(id);
            _context.Replies.Remove(replies);
            await _context.SaveChangesAsync();
            return View("Deleted");
        }

        private bool RepliesExists(int id)
        {
            return _context.Replies.Any(e => e.Id == id);
        }
    }
}
