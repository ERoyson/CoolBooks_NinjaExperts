using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class MostFlagged_Reviews_ViewModel
    {
        public List<int?> MostFlagged { get; set; } = new List<int?>();
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();
        public List<int> TotalFlags { get; set; } = new List<int>();
        public FlaggedReviews? FlaggedReviews { get; set; }

    }
}