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
        public List<Reviews>? Reviews { get; set; } = new List<Reviews>();

        public int CurrentPage { get; set; } // for our partial view (pageselector)
        public int PageCount { get; set; } // for our partial view (pageselector)

        public List<FlaggedReviews>? FlaggedReviews { get; set; }
    }
}
