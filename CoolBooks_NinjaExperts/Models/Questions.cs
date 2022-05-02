namespace CoolBooks_NinjaExperts.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public Quiz Quiz { get; set; } //FK QuizId
        public int QuizId { get; set; }
        public string Question { get; set; }
        public List<QuizOptions> QuizOptions { get; set; } = new List<QuizOptions>();
        public string Answer { get; set; }

    }
}