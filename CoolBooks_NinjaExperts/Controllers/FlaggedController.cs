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
    [Authorize(Roles = "User, Moderator, Admin")]
    public class FlaggedController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public FlaggedController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> FlagReview(string userId, int reviewId, bool? isFlagged)
        {
            var oldflaggedreview = _context.FlaggedReviews.Where(x => x.ReviewId == reviewId && x.UserId == userId).FirstOrDefault();
            var newFlaggedReview = new FlaggedReviews();

            var IsFlagged = _context.Flagged.Where(x => x.IsFlagged == isFlagged).Select(x => x.Id).FirstOrDefault();
            var book = _context.Books.Where(x => x.Reviews.Any(y => y.Id == reviewId)).FirstOrDefault();
            if (oldflaggedreview != null) // update existing flagged - flag / unflag
            {
                newFlaggedReview.ReviewId = reviewId;
                newFlaggedReview.UserId = userId;
                newFlaggedReview.FlaggedId = IsFlagged;

                _context.Remove(oldflaggedreview);
                _context.Add(newFlaggedReview);
                _context.SaveChanges();

                return RedirectToAction("Details","Books", book);
            }

            // new flag
            newFlaggedReview.ReviewId = reviewId;
            newFlaggedReview.UserId = userId;
            newFlaggedReview.FlaggedId = IsFlagged;

            _context.Add(newFlaggedReview);
            _context.SaveChanges();

            return RedirectToAction("Details", "Books", book);
        }
    }
}
