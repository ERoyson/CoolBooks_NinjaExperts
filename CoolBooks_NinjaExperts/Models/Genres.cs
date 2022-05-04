using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class Genres 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Created { get; set; }
        public List<Books>? Books { get; set; } = new List<Books>(); // Many to many relationship
        
        public Genres()
        {
            Created = DateTime.Now;
        }
    }
   
}
