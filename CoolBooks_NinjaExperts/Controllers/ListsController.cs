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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CoolBooks_NinjaExperts.Controllers
{
    public class ListsController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public ListsController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        // GET: Lists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lists = await _context.Lists
                .Include(l => l.User)
                .Include(b=>b.Books)
                .ThenInclude(l => l.Authors)
                .Include(b => b.Books)
                .ThenInclude(l => l.Genres)
                .Include(b => b.Books)
                .ThenInclude(l => l.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lists == null)
            {
                return NotFound();
            }

            return View(lists);
        }

        // GET: Lists/Create
        public IActionResult Create(string test)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = user;
            return View();
        }

        // POST: Lists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ListName,UserId")] Lists lists)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _context.Add(lists);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyLists", "Contributions");
            }
            return View(lists);
        }




        // GET: Lists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lists = await _context.Lists.FindAsync(id);
            if (lists == null)
            {
                return NotFound();
            }
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            lists.UserId = user;
            return View(lists);
        }

        // POST: Lists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ListName,UserId")] Lists lists)
        {
            if (id != lists.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListsExists(lists.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MyLists", "Contributions");
            }
            ViewData["UserId"] = new SelectList(_context.UserInfo, "Id", "Id", lists.UserId);
            return View(lists);
        }

        // GET: Lists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lists = await _context.Lists
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lists == null)
            {
                return NotFound();
            }

            return View(lists);
        }

        // POST: Lists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lists = await _context.Lists.FindAsync(id);
            _context.Lists.Remove(lists);
            await _context.SaveChangesAsync();
            return RedirectToAction("MyLists", "Contributions");
        }

        private bool ListsExists(int id)
        {
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}
