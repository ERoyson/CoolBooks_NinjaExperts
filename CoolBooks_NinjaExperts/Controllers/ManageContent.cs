#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Data;
using CoolBooks_NinjaExperts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CoolBooks_NinjaExperts.Areas.Identity.Data;
using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    public class ManageContentController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public ManageContentController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }


        public IActionResult FlaggedReviews()
        {
            var flaggedReviews = _context.FlaggedReviews
                .Include(x=>x.User)
                .Include(x=>x.Review)
                .Include(x=>x.Flagged)
                .Where(x=>x.FlaggedId == 2)
                .ToList();

            if(!flaggedReviews.Any())
            {
                return View("NothingFlagged");
            }

            return View(flaggedReviews);
        }
        public IActionResult UnFlagReview([Bind("UserId, ReviewId")] FlaggedReviews flagged)
        {
            var flaggedReview = _context.FlaggedReviews.Where(x=>x.UserId == flagged.UserId && x.ReviewId == flagged.ReviewId).FirstOrDefault();
            _context.FlaggedReviews.Remove(flaggedReview);
            _context.SaveChanges();

            return RedirectToAction("FlaggedReviews");
        }
        public IActionResult BlockReview([Bind("UserId, ReviewId")] FlaggedReviews flagged)
        {
            var selectedReview = _context.Reviews.Where(x=>x.Id == flagged.ReviewId).FirstOrDefault();
            selectedReview.IsBlocked = true;

            _context.Update(selectedReview);
            _context.SaveChanges();

            return RedirectToAction("UnFlagReview", flagged);
        }
        public IActionResult DeleteReview([Bind("UserId, ReviewId")] FlaggedReviews flagged)
        {
            var selectedReview = _context.Reviews.Where(x => x.Id == flagged.ReviewId).FirstOrDefault();
            var flaggedReview = _context.FlaggedReviews.Where(x => x.UserId == flagged.UserId && x.ReviewId == flagged.ReviewId).FirstOrDefault();

            _context.FlaggedReviews.Remove(flaggedReview);
            _context.Reviews.Remove(selectedReview);
            _context.SaveChanges();

            return RedirectToAction("FlaggedReviews");
        }

        //public IActionResult FlaggedComments()
        //public IActionResult FlaggedReplies()
    }
}
