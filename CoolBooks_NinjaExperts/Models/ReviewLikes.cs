using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class ReviewLikes
    {
        public UserInfo User { get; set; }
        public Reviews Review { get; set; }
        public string UserId { get; set; } // FK,PK
       
        public int ReviewId { get; set; } // FK,PK
    }
}
