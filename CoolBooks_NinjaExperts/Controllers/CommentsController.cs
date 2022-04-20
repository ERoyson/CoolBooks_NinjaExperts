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

        // GET: CommentsController1/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id");
            return View();
        }




        // POST: CommentsController1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int currentReviewId, [Bind("Id,UserId,ReviewsId,Comment,Created")] Comments comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", comment.UserId);
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

        public ActionResult LoadPartialView()
        {
            return PartialView("_CommentForm", new Comments());
        }
    }
}
