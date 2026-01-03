using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using BordspellenApplicatie.Models.DTO;
using BordspellenApplicatie.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Controllers
{
    public class HomeController(UserManager<IdentityUser> userManager, IBoardGameNightRepository boardGameNightRepository, IUserBoardGameNightRepository userBoardGameNightRepository, IUserRepository userRepository, ILogger<HomeController> logger, IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        private readonly IUserBoardGameNightRepository _userGameNightRepository = userBoardGameNightRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BoardGameNightDto> response = new List<BoardGameNightDto>();

            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7287/api/BoardGameNight");

                httpResponseMessage.EnsureSuccessStatusCode();

                var result = await httpResponseMessage.Content.ReadFromJsonAsync<BoardGameNightsResponse>();

                response = result?.BoardGameNights ?? new List<BoardGameNightDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching board game nights from API.");
            }

            ViewBag.BannerUrl = "/banner7.jpg";

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult SignUp()
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
