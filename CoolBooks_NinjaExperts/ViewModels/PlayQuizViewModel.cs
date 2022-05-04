using CoolBooks_NinjaExperts.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class PlayQuizViewModel
    {
        public Quiz Quiz { get; set; } 
        public List<QuizOptions> QuizOptions { get; set; } // get the answers from player
        public int? QuizPoints { get; set; }
        public List<string>? result { get; set; }
        public int QuizId { get; set; } 
        public List<string>? Answers { get; set; }
        public DateTime? StartTime { get; set; }
        public double? TotalTime { get; set; }
        public SelectList? Books { get; set; } // for a select in create function
    }
}
