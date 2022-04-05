namespace CoolBooks_NinjaExperts.Models
{
    public class Authors
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Books> Books { get; set; } // Many to many relationship
    }
}
