using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Books Book { get; set; }
        public UserInfo User { get; set; } //Quiz Creator
        public DateTime Created { get; set; }
        public int? Rating { get; set; }
        public List<Questions>? Questions { get; set; } = new List<Questions>(); //one to many relationship
        public Quiz()
        {
            Created = DateTime.Now;
        }


    }
}
