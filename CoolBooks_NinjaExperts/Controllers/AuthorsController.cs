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
         return View();
      }
      [Authorize(Roles = "Admin")]
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,FullName,Biography,ImageId")] Authors author)
      {
         var newAuthor = new Authors();
         newAuthor = author;

         foreach (var file in Request.Form.Files)
         {
            Images img = new Images();
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            img.Image = ms.ToArray();
            ms.Close();
            ms.Dispose();
            img.Thumbnail = CreateThumbnail(img.Image);
            newAuthor.Image = img;
         }
         _context.Add(newAuthor);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }
   


      [Authorize(Roles = "Admin")]
      public byte[] CreateThumbnail(byte[] imgFile)
      {

         MemoryStream ms = new MemoryStream(imgFile);
         System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

         // convert img to thumbnail
         var thumbimg = image.GetThumbnailImage(64, 64, new System.Drawing.Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);


         // convert to byte[]
         using (var ms2 = new MemoryStream())
         {
            thumbimg.Save(ms2, image.RawFormat);
            return ms2.ToArray();
         }
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
