using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    public class OverviewController : Controller
    {
        public IActionResult BoardGameNight()
        {
            // Example data, replace this with a call to your database if necessary
            var futureEvents = new List<BoardGameNight>
            {
                new BoardGameNight { Id = 1, Title = "Game Night 1", DateTime = DateTime.Now.AddDays(7) },
                new BoardGameNight { Id = 2, Title = "Game Night 2", DateTime = DateTime.Now.AddDays(14) },
                new BoardGameNight { Id = 3, Title = "Game Night 3", DateTime = DateTime.Now.AddDays(21) },
                new BoardGameNight { Id = 4, Title = "Game Night 4", DateTime = DateTime.Now.AddDays(28) },
                new BoardGameNight { Id = 5, Title = "Game Night 5", DateTime = DateTime.Now.AddDays(35) },
                new BoardGameNight { Id = 6, Title = "Game Night 6", DateTime = DateTime.Now.AddDays(7) },
                new BoardGameNight { Id = 7, Title = "Game Night 7", DateTime = DateTime.Now.AddDays(14) },
                new BoardGameNight { Id = 8, Title = "Game Night 8", DateTime = DateTime.Now.AddDays(21) },
                new BoardGameNight { Id = 9, Title = "Game Night 9", DateTime = DateTime.Now.AddDays(28) },
                new BoardGameNight { Id = 10, Title = "Game Night 10", DateTime = DateTime.Now.AddDays(35) }
            };

            ViewBag.FutureEvents = futureEvents;
            return View();
        }

        public IActionResult OrganizedBoardGameNights()
        {
            return View();
        }

        public IActionResult RegisteredBoardGameNights()
        {
            return View();
        }

        public IActionResult Boardgame()
        {
            return View();
        }
    }
}
