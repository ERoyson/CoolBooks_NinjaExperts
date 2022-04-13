using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class CreateBookViewModel
    {
        public Books? Book { get; set; } = new Books();
        public IEnumerable<Books>? Books { get; set; } = new List<Books>(); // Display in index
        public List<SelectGenresViewModel>? ListGenres { get; set; } = new List<SelectGenresViewModel>();
    }
}
