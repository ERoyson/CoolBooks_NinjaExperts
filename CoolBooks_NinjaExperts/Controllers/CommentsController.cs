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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var coolBooks_NinjaExpertsContext = _context.Comments.Include(c => c.User);
            return View(await coolBooks_NinjaExpertsContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin, User")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment")] Comments comments)
        {
            var book = _context.Books.Where(x => x.Reviews.Any(r => r.Comments.Any(z => z.Id == id))).FirstOrDefault();
            var updatedComment = _context.Comments.Where(r => r.Id == id).FirstOrDefault();
            updatedComment.Comment = comments.Comment;

            if (id != comments.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedComment);
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
                return RedirectToAction("Details", "Books", book);
            }
            return View(comments);

            //var book = _context.Books.Where(Books => Books.Id == id).FirstOrDefault();
            //return Redirect("Home/Index"); // Om vi vill tillbaka till den book-details vi var på (viewbag) https://social.msdn.microsoft.com/Forums/en-US/2353d780-a2d8-4c7f-9c4b-9dc3fb4c3b5a/back-to-previous-page-aspnet-core?forum=aspdotnetcore
            //ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", comments.UserId);
        }

        [Authorize(Roles = "Admin")]
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

        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comments = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();
            return View("Deleted");
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
