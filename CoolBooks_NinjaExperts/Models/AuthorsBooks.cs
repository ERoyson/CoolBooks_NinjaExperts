using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks_NinjaExperts.Models
{
    public class AuthorsBooks
    {
    //    [ForeignKey("Books")]
        public int BooksId { get; set; }
        //public Books Books { get; set; }

        //[ForeignKey("Authors")]
        public int AuthorsId { get; set; }
        //public Authors Authors { get; set; }
    }
}
