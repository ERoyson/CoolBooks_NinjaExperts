using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks_NinjaExperts.Models
{
    public class AuthorsBooks
    {
        public int BooksId { get; set; }
        public int AuthorsId { get; set; }
    }
}
