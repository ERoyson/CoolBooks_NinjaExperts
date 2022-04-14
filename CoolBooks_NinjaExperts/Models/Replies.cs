using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class Replies
    {
        public int Id { get; set; }
        public UserInfo User { get; set; }
        public Comments Comments { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
        public ICollection<ReplyLikes> ReplyLikes { get; set; }//many to many = list of likes on this Reply
        public ICollection<ReplyDislikes> ReplyDislikes { get; set; }//many to many = list of likes on this Reply
    }
}
