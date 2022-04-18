using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class CommentLikes
    {
        public UserInfo User { get; set; }
        public Comments Comment { get; set; }
        public string UserId { get; set; } // FK,PK
        public int CommentId { get; set; } // FK,PK
    }
}
