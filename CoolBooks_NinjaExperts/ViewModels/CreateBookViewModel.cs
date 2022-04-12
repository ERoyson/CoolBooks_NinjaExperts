using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class CreateBookViewModel
    {
        public Books? Book { get; set; }
        public List<SelectGenresViewModel>? ListGenres { get; set; } = new List<SelectGenresViewModel>();
    }
}
