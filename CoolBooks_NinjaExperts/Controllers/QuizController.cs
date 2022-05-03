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
            var quiz =  _context.Quiz.Include(q => q.Questions).ThenInclude(q => q.QuizOptions).ToList();
            


            return View(quiz);
        }

        // GET: Quizs/Details/5
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
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Book")] Quiz quiz)
        {
            var book = _context.Books.FirstOrDefault(x => x.Title == quiz.Book.Title);
            quiz.Book = book;
            quiz.User = _context.UserInfo.FirstOrDefault(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            quiz.UserId = quiz.User.Id;
            quiz.BookId = quiz.Book.Id;
         
            
                _context.Add(quiz);
                await _context.SaveChangesAsync();
            var model = new List<Questions> {  new QuizOptions() };
            return View("AddQuestions", new Questions());
            
            //return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuestion([Bind("Question,QuizOptions,Answer")] Questions questions, int id)
        {
            

            return View() ;
        }

        [HttpPost]
        public void PassThings(string[,] results)
        {
           // List<questionAnswer> questionAnswer = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<questionAnswer>>(Request.Form["hfSelected"]);

        }

        // GET: Quizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Created,Rating")] Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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

        // GET: Quizs/Delete/5
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

        // POST: Quizs/Delete/5
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
