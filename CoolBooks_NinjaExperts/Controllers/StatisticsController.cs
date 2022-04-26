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
			//var genres = _context.Genres.ToList();

			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("USA", 121));
			dataPoints.Add(new DataPoint("Great Britain", 67));
			dataPoints.Add(new DataPoint("China", 70));
			dataPoints.Add(new DataPoint("Russia", 56));
			dataPoints.Add(new DataPoint("Germany", 42));
			dataPoints.Add(new DataPoint("Japan", 41));
			dataPoints.Add(new DataPoint("France", 42));
			dataPoints.Add(new DataPoint("South Korea", 21));

			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
		}


	}
}

//Det skall finnas en grafisk uppföljning av antal kommentarer per dag och vecka, dels totalt, dels per genre och författare.

//Alltså, vi ska kunna filtrera på författare och genre, och få fram statistik på antal kommentarer per dag och vecka.
//Ex: Författare J.K. Rowling har fått 5 nya kommentarer under dagen och sammanlagt 30 nya kommentarer under veckan.
