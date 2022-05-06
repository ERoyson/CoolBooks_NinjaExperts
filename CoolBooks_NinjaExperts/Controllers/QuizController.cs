#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Data;
using CoolBooks_NinjaExperts.Models;
using CoolBooks_NinjaExperts.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CoolBooks_NinjaExperts.Controllers
{
    [Authorize(Roles ="User, Admin, Moderator")]
    public class QuizController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public QuizController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }

        // GET: Quizs
        public IActionResult Index() 
        {
            var quiz = _context.Quiz.Include(x=>x.Book).Include(y=>y.User).Where(x=>x.Questions.Count()>0).ToList();
            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //orderby?
            return View(quiz);
        }
        public IActionResult Scoreboard(int Id)
        {
            var scoreboard = _context.QuizScoreboards.Include(x => x.User).Where(x=>x.QuizId == Id).OrderByDescending(x=>x.Score).ThenBy(x=>x.Time).Take(10).ToList();
            
            return View(scoreboard);
        }

        public IActionResult PlayQuiz(int Id)
        {
            var VM = new PlayQuizViewModel();
            // SELECT QUIZ BY ID
            var options = _context.QuizOptions.Where(q => q.Question.QuizId == Id).ToList();

            VM.Quiz = _context.Quiz.Include(q => q.Questions).ThenInclude(q => q.QuizOptions).Where(x=>x.Id == Id).FirstOrDefault();
            VM.QuizOptions = new List<QuizOptions>();

            foreach (var item in options)
            {
                var q = new QuizOptions();
                q.Id = item.Id;
                q.Option = item.Option;
                q.IsSelected = false;

                VM.QuizOptions.Add(q);
            }
            VM.StartTime = DateTime.Now;

            return View(VM);
        }
        [HttpPost]
        public IActionResult SubmitQuizAnswers (List<string> result, int quizId, DateTime startTime)
        {
            DateTime endTime = DateTime.Now;
            var diffInSeconds = (endTime - startTime).TotalSeconds;
            diffInSeconds = Math.Round(diffInSeconds, 2);

            var VM = new PlayQuizViewModel();

            var answers = _context.Questions.Where(q=>q.QuizId == quizId).Select(q => q.Answer).ToList();
            int quizPoints = 0;
            for(int i = 0; i < answers.Count; i++) // compare quizresult with answers, count points per correct answer
            {
                if(result[i] == answers[i])
                {
                    quizPoints++;
                }
            }

            VM.QuizPoints = quizPoints;
            VM.result = result;
            VM.QuizId = quizId;
            VM.Answers = answers;
            VM.TotalTime = diffInSeconds;


            // adds user to scoreboard on this quiz.
            var userScore = new QuizScoreboard();
            userScore.Score = quizPoints;
            userScore.QuizId = quizId;
            userScore.Time = diffInSeconds;
            userScore.User = _context.UserInfo.FirstOrDefault(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            _context.QuizScoreboards.Add(userScore);
            _context.SaveChanges();


            return View("ShowResult", VM); // change to the correct page
        }
    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizs/Create
        public IActionResult Create()
        {
            var VM = new PlayQuizViewModel();

            VM.Books = new SelectList(_context.Books.ToList(), "Id","Title");
            ViewBag.Message = "";
            return View(VM);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Book")] Quiz quiz, int BookId)
        {
            var VM = new PlayQuizViewModel();
            VM.Books = new SelectList(_context.Books.ToList(), "Id", "Title");
            var book = _context.Books.FirstOrDefault(x => x.Id == BookId);
            quiz.Book = book;
            quiz.User = _context.UserInfo.FirstOrDefault(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            quiz.UserId = quiz.User.Id;
            VM.Quiz = quiz;
            if (BookId == 0)
            {
                ViewBag.Message = "Please select a book";
                return View(VM);
            }
            else
            {
                quiz.BookId = quiz.Book.Id;
            }
         
            
                _context.Add(quiz);
                await _context.SaveChangesAsync();

            var model = new Questions { QuizId = quiz.Id };
            return View("AddQuestions", model);
            
        }
        [Authorize]
        public IActionResult EditAddQuestions (int quizId)
        {
            var model = new Questions { QuizId = quizId };
            return View("AddQuestions", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuestions([Bind("Question,Answer")] Questions questions, int QuizId, List<string> options, string buttonSelect)
        {
            if(questions.Question == null)
            {
                questions.QuizId = QuizId;
                return View(questions);
            }
            if(options.Contains(null))
            {
                questions.QuizId = QuizId;
                return View(questions);
            }
            if (questions.Answer == null)
            {
                questions.Quiz = _context.Quiz.FirstOrDefault(Quiz => Quiz.Id == QuizId);
                questions.Answer = options[0]; //Om en fråga inte har ett svar får den automatiskt ett svar
                //questions.QuizId = QuizId;
            }
            else
            {
                questions.Quiz = _context.Quiz.FirstOrDefault(Quiz => Quiz.Id == QuizId);
                int answer = int.Parse(questions.Answer);
                questions.Answer = options[answer];
                //questions.QuizId = QuizId;
            }

            foreach(var opt in options)
            {
                var option = new QuizOptions();
                option.Option = opt;
                option.IsSelected = false;
                option.QuestionId = questions.Id;

                questions.QuizOptions.Add(option);
            }

            //questions.QuizId = QuizId;
            _context.Questions.Add(questions);
            _context.SaveChanges();

            switch(buttonSelect)
            {
                case "Add Questions":
                    {
                        var model = new Questions { QuizId = QuizId };
                        return View("AddQuestions", model);
                    }
                case "Finish":
                    {
                        return RedirectToAction("Index");
                    }
                default:
                    return RedirectToAction("Index");
            }
            
        }

        [Authorize]
        public  IActionResult Edit(int? id)
        {
           
            
                if (id == null)
                {
                    return NotFound();
                }

                var quiz = _context.Quiz.Where(q => q.Id == id)
                    .Include(q => q.Questions)
                    .ThenInclude(q => q.QuizOptions)
                    .FirstOrDefault();
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (user == quiz.UserId || User.IsInRole("Admin, Moderator"))
            {
                if (quiz == null)
                {
                    return NotFound();
                }
                return View(quiz);
            }


            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Questions")] Quiz quiz)
        {

            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);
            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.Id == id);
        }
    }
}
