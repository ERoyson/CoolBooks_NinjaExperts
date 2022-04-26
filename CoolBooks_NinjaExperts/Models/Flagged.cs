namespace CoolBooks_NinjaExperts.Models
{
    public class Flagged
    {
        public int Id { get; set; }
        public bool IsFlagged { get; set; }
        public ICollection<FlaggedReviews>? FlaggedReviews { get; set; } //many to many
        public ICollection<FlaggedComments>? FlaggedComments { get; set; } //many to many
    }
}
