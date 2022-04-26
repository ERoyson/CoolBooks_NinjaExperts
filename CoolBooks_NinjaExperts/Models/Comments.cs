using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class Comments
    {
        public int Id { get; set; }// PK
        public UserInfo? User { get; set; } // FK
        public string? UserId { get; set; } // FK UserId
        public Reviews? Reviews { get; set; } // FK
        public int ReviewsId { get; set; } // FK ReviewId
        public List<Replies>? Replies { get; set; } = new List<Replies>(); 
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
        public ICollection<CommentLikes>? CommentLikes { get; set; }//many to many = list of likes on this Comment
        public ICollection<CommentDislikes>? CommentDislikes { get; set; }//many to many = list of likes on this Comment
        public ICollection<FlaggedComments>? FlaggedComments { get; set; } //many to many

        public bool? IsBlocked { get; set; }

        public Comments()
        {
            Created = DateTime.Now;
        }
    }
}
