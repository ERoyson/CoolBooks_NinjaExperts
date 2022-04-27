using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Data;
using CoolBooks_NinjaExperts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Collections.Generic;
using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.Models
{
    [Authorize(Roles = "Admin, Moderator")]
    public class StatisticsController : Controller
    {
		private readonly CoolBooks_NinjaExpertsContext _context;

		public StatisticsController(CoolBooks_NinjaExpertsContext context)
		{
			_context = context;
		}


		// GET: Home
		public ActionResult Index()
		{
            List<DataPoint> dataPoints = new List<DataPoint>();

            var VM = new ShowStatisticsViewModel();
            VM.Genres = _context.Genres.ToList();
            VM.Authors = _context.Authors.ToList();

            VM.Comments = _context.Comments
                .Include(c => c.Reviews)
                .ThenInclude(c => c.Book)
                .ThenInclude(c => c.Genres)
                .ThenInclude(bg => bg.Books)
                .ToList();

            //Summering av dagens kommentarer
            var commentsToday = VM.Comments.Where(c => c.Created.Date == DateTime.Today).Count();

            //Summering av veckans kommentarer
            var commentsWeek = VM.Comments.Where(c => c.Created.Date <= DateTime.Today || c.Created.Date >= DateTime.Today.AddDays(-7)).Count();

            //Summering av alla kommentarer
            var commentsTotal = VM.Comments.Count();

            dataPoints.Add(new DataPoint("Today", commentsToday));
            dataPoints.Add(new DataPoint("This week", commentsWeek));
            dataPoints.Add(new DataPoint("Total", commentsTotal));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View(VM);
		}

		public ActionResult ExampleChart()
        {
			List<DataPoint> dataPoints = new List<DataPoint>();
            //var genres = _context.Genres.ToList();
            //foreach (var item in genres)
            //{
            //	dataPoints.Add(new DataPoint(item.Name, 121));
            //}
            dataPoints.Add(new DataPoint("USA", 121));
            dataPoints.Add(new DataPoint("Great Britain", 67));
            dataPoints.Add(new DataPoint("China", 70));
            dataPoints.Add(new DataPoint("Russia", 56));
            dataPoints.Add(new DataPoint("Germany", 42));
            dataPoints.Add(new DataPoint("Japan", 41));
            dataPoints.Add(new DataPoint("France", 42));
            dataPoints.Add(new DataPoint("South Korea", 21));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            //return RedirectToAction("Index", dataPoints);
            return View();

		}
        public ActionResult GenresStats()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            var comments = _context.Comments.ToList();
            var reviews = _context.Reviews.ToList();
            var books = _context.Books.ToList();
            var genres = _context.Genres.ToList();
            var VM = new ShowStatisticsViewModel();

            VM.Comments = _context.Comments
                .Include(c => c.Reviews)
                .ThenInclude(c => c.Book)
                .ThenInclude(c => c.Genres)
                .ThenInclude(bg => bg.Books)
                .ToList();


            int sum = 0;
            //foreach (var item in genres)
            //{
            //    //LINQ för att hämta ut kommentar för nuvarande genre
            //    //WHERE ... bookgenre.Genres == item.Name
            //    //börja inifrån och ut

            //    //Hämta kommentarer där reviewsbookgenres någon genre som har samma genre
            //    var commentGenres = VM.Comments.Where(c => c.Reviews.Book.Genres.Any(g => g.Name == item.Name) && c.Created.Date == DateTime.Today).Distinct().ToList();
            //    int totalNumberPerGenre = commentGenres.Count();
            //    sum = sum + totalNumberPerGenre;
            //    dataPoints.Add(new DataPoint(item.Name, totalNumberPerGenre));
            //}
            //dataPoints.Add(new DataPoint("Today", sum));


            //return RedirectToAction("Index", dataPoints);
            return View();

        }


        public ActionResult CreateGenreGraph(string genre)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            var VM = new ShowStatisticsViewModel();

            VM.Comments = _context.Comments
                .Include(c => c.Reviews)
                .ThenInclude(c => c.Book)
                .ThenInclude(c => c.Genres)
                .ThenInclude(bg => bg.Books)
                .ToList();

            var dailyComments = VM.Comments.Where(c => c.Reviews.Book.Genres.Any(g => g.Name == genre) && c.Created.Date == DateTime.Today).ToList();
            var weeklyComments = VM.Comments.Where(c => c.Reviews.Book.Genres.Any(g => g.Name == genre) && (c.Created.Date <= DateTime.Today || c.Created.Date >= DateTime.Today.AddDays(-7))).ToList();
            var totalComments = VM.Comments.Where(c => c.Reviews.Book.Genres.Any(g => g.Name == genre)).ToList();

            int daily = dailyComments.Count();
            int weekly = weeklyComments.Count();
            int total = totalComments.Count();

            dataPoints.Add(new DataPoint("Today", daily));
            dataPoints.Add(new DataPoint("This week", weekly));
            dataPoints.Add(new DataPoint("Total", total));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.GenreName = genre;

            return View();
        }

        public ActionResult CreateAuthorGraph(string author)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            var VM = new ShowStatisticsViewModel();

            VM.Comments = _context.Comments
                .Include(c => c.Reviews)
                .ThenInclude(c => c.Book)
                .ThenInclude(c => c.Authors)
                .ThenInclude(bg => bg.Books)
                .ToList();

            var dailyComments = VM.Comments.Where(c => c.Reviews.Book.Authors.Any(g => g.FullName == author) && c.Created.Date == DateTime.Today).ToList();
            var weeklyComments = VM.Comments.Where(c => c.Reviews.Book.Authors.Any(g => g.FullName == author) && (c.Created.Date <= DateTime.Today || c.Created.Date >= DateTime.Today.AddDays(-7))).ToList();
            var totalComments = VM.Comments.Where(c => c.Reviews.Book.Authors.Any(g => g.FullName == author)).ToList();

            int daily = dailyComments.Count();
            int weekly = weeklyComments.Count();
            int total = totalComments.Count();

            dataPoints.Add(new DataPoint("Today", daily));
            dataPoints.Add(new DataPoint("This week", weekly));
            dataPoints.Add(new DataPoint("Total", total));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.AuthorName = author;

            return View();
        }

    }
}

//Det skall finnas en grafisk uppföljning av antal kommentarer per dag och vecka, dels totalt, dels per genre och författare.

//Alltså, vi ska kunna filtrera på författare och genre, och få fram statistik på antal kommentarer per dag och vecka.
//Ex: Författare J.K. Rowling har fått 5 nya kommentarer under dagen och sammanlagt 30 nya kommentarer under veckan.
