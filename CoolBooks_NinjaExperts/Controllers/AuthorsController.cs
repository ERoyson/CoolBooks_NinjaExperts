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
using Microsoft.AspNetCore.Authorization;

namespace CoolBooks_NinjaExperts.Models
{
    
    public class AuthorsController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public AuthorsController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }



       
        public IActionResult Index(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var authors = _context.Authors
                .Include(a => a.Image)
                .Include(a => a.Books)
                .ToList();

            switch (sortOrder)
            {
                case "name_desc":
                    authors = authors.OrderByDescending(a => a.FullName).ToList();
                    break;
                default:
                    authors = authors.OrderBy(a => a.FullName).ToList();
                    break;


            }
                    return View(authors);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors
                .Include(a => a.Image)
                .Include(a => a.Books)
                .ThenInclude(b => b.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authors == null)
            {
                return NotFound();
            }

            return View(authors);
        }

       
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Biography,Created,ImageId")] Authors authors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", authors.ImageId);
            return View(authors);
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors.FindAsync(id);
            if (authors == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", authors.ImageId);
            return View(authors);
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Biography,Created,ImageId")] Authors authors)
        {
            if (id != authors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorsExists(authors.Id))
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
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", authors.ImageId);
            return View(authors);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors
                .Include(a => a.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authors == null)
            {
                return NotFound();
            }

            return View(authors);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authors = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(authors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorsExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
