using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class DisplayBooksViewModel
    {
        public Books? Book { get; set; } = new Books();
        public IEnumerable<Books>? Books { get; set; } = new List<Books>(); // Display in index
        public IEnumerable<Books>? RandomBooks { get; set; } = new List<Books>(); // Display RandomBooks on mainpage
        public int PageCount { get; set; } // switch between pages
    }
}
