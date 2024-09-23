using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BordspellenApplicatie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Example data, replace this with a call to your database if necessary
            var futureEvents = new List<BoardGameNight>
            {
                new BoardGameNight { Title = "Game Night 1", Date = DateTime.Now.AddDays(7), Is18Plus = true },
                new BoardGameNight { Title = "Game Night 2", Date = DateTime.Now.AddDays(14), Is18Plus = true },
                new BoardGameNight { Title = "Game Night 3", Date = DateTime.Now.AddDays(21), Is18Plus = true },
                new BoardGameNight { Title = "Game Night 4", Date = DateTime.Now.AddDays(28), Is18Plus = true },
                new BoardGameNight { Title = "Game Night 5", Date = DateTime.Now.AddDays(35), Is18Plus = true }
            };

            ViewBag.BannerUrl = "/imageBoardgame.jpg";
            ViewBag.InfoText = "Details about what the board game nights are.";
            ViewBag.FutureEvents = futureEvents;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
