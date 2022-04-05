namespace CoolBooks_NinjaExperts.Models
{
    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Books> Books { get; set; } // Many to many relationship
    }
}
