using CoolBooks_NinjaExperts.Areas.Identity.Data;
using CoolBooks_NinjaExperts.Models;
using Microsoft.AspNetCore.Identity;

namespace CoolBooks_NinjaExperts.ViewModels
{
    public class ContributionsViewModel
    {
        public Books? Book { get; set; }
        public int BookId { get; set; }
        public UserInfo? User { get; set; }
        public string UserId { get; set; }
        public Reviews? Review { get; set; }
        public int ReviewId { get; set; }
        public Comments? Comment { get; set; }
        public int CommentId { get; set; }
        public Replies? Reply { get; set; }
        public int ReplyId { get; set; }
        public Lists? List { get; set; }
        public int ListId { get; set; }
        public Images? Image { get; set; }
        public int ImageId { get; set; }
        public Quiz? Quiz { get; set; }
        public int QuizId { get; set; }


        public List<Books>? Books { get; set; } = new List<Books>();
        public List<Authors>? Authors { get; set; } = new List<Authors>();
        public List<Genres>? Genres { get; set; } = new List<Genres>();
        public List<Images>? Images { get; set; } = new List<Images>();
        public List<Reviews>? Reviews { get; set; } = new List<Reviews>();
        public List<Comments>? Comments { get; set; } = new List<Comments>();
        public List<Replies>? Replies { get; set; } = new List<Replies>();
        public List<Lists>? Lists { get; set; } = new List<Lists>();
        public List<Quiz>? Quizzes { get; set; } = new List<Quiz>();

    } 
}
