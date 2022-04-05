using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public UserInfo User { get; set; } // Fk UserId - AspNetUsers
        public Books Book { get; set; } // FK BookId
        public string Title { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
    }
}
