using CoolBooks_NinjaExperts.Areas.Identity.Data;
using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class BookReviewsViewModel
    {
        public Books? Book { get; set; }
        public UserInfo? User { get; set; }
        public string UserId { get; set; }
        public Reviews? Review { get; set; }
        public string ReviewId { get; set; }
        public Comments? Comment { get; set; }
        public int CommentId { get; set; }
        public Replies? Reply { get; set; }
        public List<Reviews>? Reviews { get; set; } = new List<Reviews>();
        public List<Comments>? Comments { get; set; } = new List<Comments>();
        public List<Replies>? Replies { get; set; } = new List<Replies>();

        public int CurrentPage { get; set; } // for our partial view (pageselector)
        public int PageCount { get; set; } // for our partial view (pageselector)

        public List<FlaggedReviews>? FlaggedReviews { get; set; }
        public List<FlaggedComments>? FlaggedComments { get; set; }
        public List<ReviewLikes>? ReviewLikes { get; set; }
        public List<ReviewDislikes>? ReviewDislikes { get; set; }
    }
}
