using CoolBooks_NinjaExperts.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks_NinjaExperts.Models
{
    public class Books
    {
        public int Id { get; set; }

        public UserInfo? User { get; set; } // Fk UserId - AspNetUser

        [Required]
        [StringLength(255, ErrorMessage = "The Book-Title must be less than 255 characters")]

        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Published { get; set; }

        [Required]
        public long ISBN { get; set; }


        public double? Rating { get; set; }

        public Images? Image { get; set; } // FK ImageId
        public int? ImageId { get; set; } //FK
        
        [DataType(DataType.Date)] //Visar endast datum och ej tid
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Deleted { get; set; }
        public string BookSeries { get; set; } // Om fler böcker ska finnas i samma serie - lätt att söka

        public List<Genres>? Genres { get; set; } // many to many relationship
        public int? GenreId { get; set; }//FK
        public List<Authors>? Authors { get; set; } // many to many relationship
        public int? AuthorId { get; set; } //FK
      
        public Books()
          {
              this.Created = DateTime.Now;
          }


    }
}
