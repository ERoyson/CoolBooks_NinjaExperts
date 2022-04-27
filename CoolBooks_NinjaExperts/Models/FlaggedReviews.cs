using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class FlaggedReviews
    {
        public Flagged? Flagged { get; set; }
        public Reviews? Review { get; set; }
        public UserInfo? User { get; set; }
        public DateTime? Created { get; set; }
        public int? FlaggedId { get; set; }
        public int? ReviewId { get; set; }
        public string? UserId { get; set; }
        public FlaggedReviews()
        {
            Created = DateTime.Now;
        }
    }
}
