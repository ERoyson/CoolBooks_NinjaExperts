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
    public class ManageContentController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public ManageContentController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }
        //REVIEWS
        //------------------------------------------
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult FlaggedReviews()
        {
            var flaggedReviews = _context.FlaggedReviews
                .Include(x => x.User)
                .Include(x => x.Review)
                .Include(x => x.Flagged)
                .Where(x => x.FlaggedId == 2)
                .ToList();

            if (!flaggedReviews.Any())
            {
                return View("NothingFlagged");
            }

            return View(flaggedReviews);
        }
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult UnFlagReview([Bind("UserId, ReviewId")] FlaggedReviews flagged)
        {
            var flaggedReview = _context.FlaggedReviews.Where(x => x.UserId == flagged.UserId && x.ReviewId == flagged.ReviewId).FirstOrDefault();
            _context.FlaggedReviews.Remove(flaggedReview);
            _context.SaveChanges();

            return RedirectToAction("FlaggedReviews");
        }
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult BlockReview([Bind("UserId, ReviewId")] FlaggedReviews flagged)
        {
            var selectedReview = _context.Reviews.Where(x => x.Id == flagged.ReviewId).FirstOrDefault();
            selectedReview.IsBlocked = true;

            _context.Update(selectedReview);
            _context.SaveChanges();

            return RedirectToAction("UnFlagReview", flagged);
        }
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult DeleteReview([Bind("UserId, ReviewId")] FlaggedReviews flagged)
        {
            var selectedReview = _context.Reviews.Where(x => x.Id == flagged.ReviewId).FirstOrDefault();
            var flaggedReview = _context.FlaggedReviews.Where(x => x.UserId == flagged.UserId && x.ReviewId == flagged.ReviewId).FirstOrDefault();

            _context.FlaggedReviews.Remove(flaggedReview);
            _context.Reviews.Remove(selectedReview);
            _context.SaveChanges();

            return RedirectToAction("FlaggedReviews");
        }

      

        [Authorize(Roles = "User, Admin, Moderator")]
        public IActionResult LikeReviews(bool check, int reviewId)
        {
            var book = _context.Books.FirstOrDefault(x => x.Reviews.Any(r => r.Id == reviewId));
            var RL = new ReviewLikes();
            RL.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (check == false)
            {
                RL = _context.ReviewLikes.FirstOrDefault(x => x.UserId == RL.UserId && x.ReviewId == reviewId);
                _context.Remove(RL);
            }
            if (check == true)
            {
                // ADD TO REVIEWLIKES

                RL.ReviewId = reviewId;
                _context.Add(RL);

                // REMOVE FROM DISLIKES IF EXISTS
                var RDL = _context.ReviewDislikes.FirstOrDefault(x => x.UserId == RL.UserId && x.ReviewId == reviewId);
                if (RDL != null)
                {
                    _context.ReviewDislikes.Remove(RDL);
                }
            }
            _context.SaveChanges();

            return RedirectToAction("Details", "Books", book);
        }
        [Authorize(Roles = "User, Admin, Moderator")]
        public IActionResult DislikeReviews(bool check, int reviewId)
        {
            var book = _context.Books.FirstOrDefault(x => x.Reviews.Any(r => r.Id == reviewId));
            var RDL = new ReviewDislikes();
            RDL.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get userID

            if (check == false)
            {
                RDL = _context.ReviewDislikes.FirstOrDefault(x => x.UserId == RDL.UserId && x.ReviewId == reviewId);
                _context.Remove(RDL);
            }
            if (check == true)
            {
                // ADD TO REVIEWDISLIKES
                RDL.ReviewId = reviewId;
                _context.Add(RDL);

                // REMOVE FROM DISLIKES IF EXISTS
                var RL = _context.ReviewLikes.FirstOrDefault(x => x.UserId == RDL.UserId && x.ReviewId == reviewId);
                if (RL != null)
                {
                    _context.ReviewLikes.Remove(RL);
                }
            }
            _context.SaveChanges();

            return RedirectToAction("Details", "Books", book);
        }
        [Authorize(Roles = "User, Admin, Moderator")]
        public IActionResult LikeComments()
        {
            return View();
        }
        [Authorize(Roles = "User, Admin, Moderator")]
        public IActionResult DislikeComments()
        {
            return View();
        }
        [Authorize(Roles = "User, Admin, Moderator")]
        public IActionResult LikeReplies()
        {
            return View();
        }
        [Authorize(Roles = "User, Admin, Moderator")]
        public IActionResult DislikeReplies()
        {
            return View();
        }

        //COMMENTS
        // -------------------------------------------
        public IActionResult FlaggedComments()
        {
            var FlaggedComments = _context.FlaggedComments
                .Include(x => x.User)
                .Include(x => x.Comments)
                .Include(x => x.Flagged)
                .Where(x => x.FlaggedId == 2)
                .ToList();

            if (!FlaggedComments.Any())
            {
                return View("NothingFlagged");
            }

            return View(FlaggedComments);
        }

        // -------------------------------------------
        //public IActionResult FlaggedReplies()

        // -------------------------------------------


    }

}
