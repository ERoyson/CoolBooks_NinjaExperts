namespace CoolBooks_NinjaExperts.Models
{
    public class QuizOptions
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public bool IsSelected { get; set; }
        public Questions Question { get; set; }
        public int QuestionId { get; set; }
    }
}