using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class Books
    {
        public int Id { get; set; }
        public UserInfo User { get; set; } // Fk UserId - AspNetUsers
        public string Title { get; set; }
        public string Description { get; set; }
        public long ISBN { get; set; }
        public double Rating { get; set; }
        public Images Image { get; set; } // FK ImageId 
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public virtual ICollection<Genres> Genres { get; set; } // many to many relationship
        public virtual ICollection<Authors> Authors { get; set; } // many to many relationship
    }
}
