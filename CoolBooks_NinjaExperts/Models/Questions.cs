namespace CoolBooks_NinjaExperts.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public Quiz Quiz { get; set; } //FK QuizId
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }

    }
}
