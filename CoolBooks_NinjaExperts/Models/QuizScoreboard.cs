using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Models
{
    public class QuizScoreboard
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public UserInfo User { get; set; }
        public int Score { get; set; }
        public double Time { get; set; }
    }
}
