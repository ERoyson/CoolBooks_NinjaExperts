using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public UserInfo? User { get; set; } // Fk UserId - AspNetUsers
        public string? UserId { get; set; } // FK UserId
        public Books? Book { get; set; } // FK BookId
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; } // Give the book a rating
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }

        public ICollection<ReviewLikes>? ReviewLikes { get; set; }//many to many = list of likes on this review
        public ICollection<ReviewDislikes>? ReviewDislikes { get; set; }//many to many = list of likes on this review
        public ICollection<FlaggedReviews>? FlaggedReviews { get; set; } //many to many

        public bool? IsBlocked { get; set; }

        public Reviews()
        {
            Created = DateTime.Now;
        }

    }
}
