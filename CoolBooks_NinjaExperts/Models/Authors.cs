﻿namespace CoolBooks_NinjaExperts.Models
{
    public class Authors
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Biography { get; set; }
        public DateTime Created { get; set; }
        public Images? Image { get; set; } // Author images // FK
        public int? ImageId { get; set; } //FK

        public List<Books>Books { get; set; } // Many to many relationship

        public Authors()
        {
            this.Created = DateTime.Now;
        }
    }
}
