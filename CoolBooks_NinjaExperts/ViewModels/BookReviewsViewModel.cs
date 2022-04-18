using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class BookReviewsViewModel
    {
        public Books Book { get; set; }
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();

        public int CurrentPage { get; set; } // for our partial view (pageselector)
        public int PageCount { get; set; } // for our partial view (pageselector)
    }
}
