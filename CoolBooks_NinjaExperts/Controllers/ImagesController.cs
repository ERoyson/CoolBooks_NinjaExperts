#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Data;
using Microsoft.AspNetCore.Authorization;

namespace CoolBooks_NinjaExperts.Models
{
    public class ImagesController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public ImagesController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        // GET: Images
        [Authorize(Roles = "Admin, Moderator, User")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var images = await _context.Images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // GET: Images/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(bool Notuseful)
        {
            foreach (var file in Request.Form.Files)
            {
                Images img = new Images();

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                img.Image = ms.ToArray();

                ms.Close();
                ms.Dispose();

                // --
                img.Thumbnail = CreateThumbnail(img.Image);
                // --

                _context.Images.Add(img);
                _context.SaveChanges();
            }
            ViewBag.Message = "Image(s) stored in database!";
            return View("Index");
        }

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




        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Thumbnail")] Images images)
        {


            if (ModelState.IsValid)
            {
                _context.Add(images);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(images);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); //Returnerar att sidan ej finns
            }

            var images = await _context.Images.FindAsync(id);
            if (images == null)
            {
                return NotFound();
            }
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Thumbnail")] Images images)
        {
            if (id != images.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(images);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagesExists(images.Id))
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
            return View(images);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var images = await _context.Images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var images = await _context.Images.FindAsync(id);
            _context.Images.Remove(images);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagesExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
