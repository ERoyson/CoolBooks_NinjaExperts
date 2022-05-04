using CoolBooks_NinjaExperts.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks_NinjaExperts.Models
{
    public class Lists
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public DateTime Created { get; set; }
        public UserInfo? User { get; set; } //FK UserId
        public string UserId { get; set; } //FK

        public List<Books> Books { get; set; } = new List<Books>(); // Many to many relationship

        public Lists()
        {
            this.Created = DateTime.Now;
        }
    }
}
