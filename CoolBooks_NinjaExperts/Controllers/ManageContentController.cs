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
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult FlaggedComments(string orderby = "")
        {
            IEnumerable<FlaggedComments> FlaggedComments = new List<FlaggedComments>();

            
            switch (orderby)
            {
                case "Newest":
                    {
                        FlaggedComments = _context.FlaggedComments
                            .Include(x => x.User)
                            .Include(x => x.Comments)
                            .Include(x => x.Flagged)
                            .Where(x => x.FlaggedId == 2)
                            .OrderByDescending(x=>x.Created)
                            .ToList();
                        break;
                    }
                    
                case "MostFlagged":
                    {
                        var VM = new MostFlagged_Comments_ViewModel();

                        IEnumerable<int?> Ie_mostFlagged = _context.FlaggedComments
                       .GroupBy(i => i.CommentId)
                       .OrderByDescending(g => g.Count())
                       .Take(10)
                       .Select(g => g.Key)
                       .AsEnumerable();

                        
                        List<int?> mostFlagged = Ie_mostFlagged.ToList();

                        VM.MostFlagged = mostFlagged;

                        foreach(var flagged in mostFlagged)
                        {
                            var totalflags = _context.FlaggedComments.Where(x => x.CommentId == flagged.Value).Count(); // count total amount of flags on comment
                            var comment = _context.Comments.Where(c => c.Id == flagged.Value).FirstOrDefault();
                            VM.Comments.Add(comment);
                            VM.TotalFlags.Add(totalflags); 
                        }
                        return View("FlaggedComments_MostFlagged", VM);
                        
                    }
                case "":
                    {
                        FlaggedComments = _context.FlaggedComments
                            .Include(x => x.User)
                            .Include(x => x.Comments)
                            .Include(x => x.Flagged)
                            .Where(x => x.FlaggedId == 2)
                            .ToList();
                        break;
                    }
                default:
                    break;
            }
            
            if (!FlaggedComments.Any())
            {
                return View("NothingFlagged");
            }

            return View(FlaggedComments);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult UnFlagComment([Bind("UserId, CommentId")] FlaggedComments flagged)
        {
            if(flagged.UserId == null)
            {
                var unflagAll = _context.FlaggedComments.Where(x => x.CommentId == flagged.CommentId).ToList(); // takes all flags on this comment and unflag it
                foreach (var comment in unflagAll)
                {
                    _context.FlaggedComments.Remove(comment);
                }
                _context.SaveChanges();
                return RedirectToAction("FlaggedComments");
            }

            var flaggedComment = _context.FlaggedComments.Where(x => x.UserId == flagged.UserId && x.CommentId == flagged.CommentId).FirstOrDefault(); // unflags a single comment with a single user
            _context.FlaggedComments.Remove(flaggedComment);
            _context.SaveChanges();

            return RedirectToAction("FlaggedComments");
        }

        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult BlockComment([Bind("UserId, CommentId")] FlaggedComments flagged)
        {
            var selectedComment = _context.Comments.Where(x => x.Id == flagged.CommentId).FirstOrDefault();
            selectedComment.IsBlocked = true;

            _context.Update(selectedComment);
            _context.SaveChanges();

            return RedirectToAction("UnFlagComment", flagged);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult DeleteComment([Bind("UserId, CommentId")] FlaggedComments flagged)
        {
            var selectedComment = _context.Comments.Where(x => x.Id == flagged.CommentId).FirstOrDefault();
            
            _context.Comments.Remove(selectedComment);
            _context.SaveChanges();

            return RedirectToAction("FlaggedComments");
        }

        [Authorize(Roles = "User, Admin, Moderator")]
        public IActionResult LikeComments(bool check, int commentId)
        {
            
            var book = _context.Books.FirstOrDefault(x => x.Reviews.Any(y => y.Comments.Any(z => z.Id == commentId)));
            var CL = new CommentLikes();
            CL.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (check == false)
            {
                CL = _context.CommentLikes.FirstOrDefault(x => x.UserId == CL.UserId && x.CommentId == commentId);
                _context.Remove(CL);
            }
            if (check == true)
            {
                // ADD TO COMMENTLIKES

                CL.CommentId = commentId;
                _context.Add(CL);

                // REMOVE FROM DISLIKES IF EXISTS
                var CDL = _context.CommentDislikes.FirstOrDefault(x => x.UserId == CL.UserId && x.CommentId == commentId);
                if (CDL != null)
                {
                    _context.CommentDislikes.Remove(CDL);
                }
            }
            _context.SaveChanges();

            return RedirectToAction("Details", "Books", book);
        }

        [Authorize(Roles = "User, Admin, Moderator")]
  
        public IActionResult DisLikeComments(bool check, int commentId)
        {                                             
            var book = _context.Books.FirstOrDefault(x => x.Reviews.Any(y => y.Comments.Any(z => z.Id == commentId)));
            var CDL = new CommentDislikes();
            CDL.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get userID

            if (check == false)
            {
                CDL = _context.CommentDislikes.FirstOrDefault(x => x.UserId == CDL.UserId && x.CommentId == commentId);
                _context.Remove(CDL);
            }
            if (check == true)
            {
                // ADD TO COMMENTDISLIKES
                CDL.CommentId = commentId;
                _context.Add(CDL);

                // REMOVE FROM LIKES IF EXISTS
                var CL = _context.CommentLikes.FirstOrDefault(x => x.UserId == CDL.UserId && x.CommentId == commentId);
                if (CL != null)
                {
                    _context.CommentLikes.Remove(CL);
                }
            }
            _context.SaveChanges();

            return RedirectToAction("Details", "Books", book);
        }
        // -------------------------------------------
        //public IActionResult FlaggedReplies()

        // -------------------------------------------


    }

}
