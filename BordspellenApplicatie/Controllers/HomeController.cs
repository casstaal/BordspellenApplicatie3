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
            ViewBag.InfoText = "Welkom op onze bordspellenavonden voor immigranten! Ben je nieuw in Nederland en wil je op een leuke en ontspannen manier de Nederlandse taal leren? Kom dan naar onze gezellige bordspellenavonden. Hier combineren we taalvaardigheid met spelplezier!\nTijdens deze avonden speel je samen met anderen verschillende bordspellen, waarbij je op een speelse manier je Nederlands oefent. Het is een perfecte gelegenheid om nieuwe mensen te ontmoeten, de taal in praktijk te brengen en spelenderwijs je woordenschat en taalbegrip te verbeteren.\nOf je nu een beginner bent of al wat verder gevorderd, iedereen is welkom. Onze vrijwilligers begeleiden de avond en helpen je graag met moeilijke woorden of zinnen. Het belangrijkste is dat je plezier hebt terwijl je leert!\nDoe mee, verbeter je Nederlands en ontdek hoe leuk taal leren kan zijn!";
            ViewBag.FutureEvents = futureEvents;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Account()
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
