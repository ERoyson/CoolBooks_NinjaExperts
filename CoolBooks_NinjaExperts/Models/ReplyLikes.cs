using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class ReplyLikes
    {
        public UserInfo User { get; set; }
        public Replies Reply { get; set; }
        public string UserId { get; set; } // FK,PK
        public int ReplyId { get; set; } // FK,PK
    }
}
