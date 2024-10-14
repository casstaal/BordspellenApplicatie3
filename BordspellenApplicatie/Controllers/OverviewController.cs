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
                new BoardGameNight { Id = 1, Title = "Game Night 1", Date = DateTime.Now.AddDays(7), Is18Plus = true },
                new BoardGameNight { Id = 2, Title = "Game Night 2", Date = DateTime.Now.AddDays(14), Is18Plus = true },
                new BoardGameNight { Id = 3, Title = "Game Night 3", Date = DateTime.Now.AddDays(21), Is18Plus = true },
                new BoardGameNight { Id = 4, Title = "Game Night 4", Date = DateTime.Now.AddDays(28), Is18Plus = true },
                new BoardGameNight { Id = 5, Title = "Game Night 5", Date = DateTime.Now.AddDays(35), Is18Plus = true },
                new BoardGameNight { Id = 6, Title = "Game Night 6", Date = DateTime.Now.AddDays(7), Is18Plus = true },
                new BoardGameNight { Id = 7, Title = "Game Night 7", Date = DateTime.Now.AddDays(14), Is18Plus = true },
                new BoardGameNight { Id = 8, Title = "Game Night 8", Date = DateTime.Now.AddDays(21), Is18Plus = true },
                new BoardGameNight { Id = 9, Title = "Game Night 9", Date = DateTime.Now.AddDays(28), Is18Plus = true },
                new BoardGameNight { Id = 10, Title = "Game Night 10", Date = DateTime.Now.AddDays(35), Is18Plus = true }
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
