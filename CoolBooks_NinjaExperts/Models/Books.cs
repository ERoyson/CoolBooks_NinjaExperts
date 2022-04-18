using CoolBooks_NinjaExperts.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks_NinjaExperts.Models
{
    public class Books
    {
        public int Id { get; set; }

        public UserInfo? User { get; set; } // Fk UserId - AspNetUser

        public string? UserId { get; set; } // FK UserId


        [Required]
        [StringLength(255, ErrorMessage = "The Book-Title must be less than 255 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Published { get; set; }

        [Required]
        public long ISBN { get; set; }


        public double? Rating { get; set; }

        public Images? Image { get; set; } // FK ImageId
        public int? ImageId { get; set; }
        
        [DataType(DataType.Date)] //Visar endast datum och ej tid
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Deleted { get; set; }


        //[Required]
        [StringLength(255, ErrorMessage = "The Book-Title must be less than 255 characters")]
        public string? BookSeries { get; set; } // Om fler böcker ska finnas i samma serie - lätt att söka

        [Required]
        public List<Genres>? Genres { get; set; } = new List<Genres>(); // many to many relationship
        [Required]
        public List<Authors>? Authors { get; set; } = new List<Authors>(); // many to many relationship

      
        public Books()
          {
              this.Created = DateTime.Now;
          }


    }
}
