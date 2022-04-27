using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class MostFlagged_Comments_ViewModel
    {
        public List<int?> MostFlagged { get; set; } = new List<int?>();
        public List<Comments> Comments { get; set; } = new List<Comments>();
        public List<int> TotalFlags { get; set; } = new List<int>();
        public FlaggedComments? FlaggedComments { get; set; }

    }
}