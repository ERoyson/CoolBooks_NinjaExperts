using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class PlayQuizViewModel
    {
        public Quiz Quiz { get; set; }
        public List<QuizOptions> QuizOptions { get; set; } // get the answers from player
    }
}
