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
using System.Security.Claims;

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

        public async Task<IActionResult> FlagReview(int reviewId, bool? isFlagged)
        {
            var newFlaggedReview = new FlaggedReviews();
            newFlaggedReview.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var oldflaggedreview = _context.FlaggedReviews.Where(x => x.ReviewId == reviewId && x.UserId == newFlaggedReview.UserId).FirstOrDefault();

            var IsFlagged = _context.Flagged.Where(x => x.IsFlagged == isFlagged).Select(x => x.Id).FirstOrDefault();
            var book = _context.Books.Where(x => x.Reviews.Any(y => y.Id == reviewId)).FirstOrDefault();

            if (oldflaggedreview != null) // update existing flagged - flag / unflag
            {
                if (oldflaggedreview.FlaggedId == IsFlagged)
                {
                    return RedirectToAction("Details", "Books", book);
                }
                newFlaggedReview.ReviewId = reviewId;
                newFlaggedReview.FlaggedId = IsFlagged;

                _context.Remove(oldflaggedreview);
                _context.Add(newFlaggedReview);
                _context.SaveChanges();

                return RedirectToAction("Details", "Books", book);
            }

            // new flag
            newFlaggedReview.ReviewId = reviewId;
            newFlaggedReview.FlaggedId = IsFlagged;

            _context.Add(newFlaggedReview);
            _context.SaveChanges();

            return RedirectToAction("Details", "Books", book);
        }
    }
}
