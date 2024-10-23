using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BordspellenApplicatie.Controllers
{
    public class HomeController(UserManager<IdentityUser> userManager, IBoardGameNightRepository boardGameNightRepository, IUserBoardGameNightRepository userBoardGameNightRepository, IUserRepository userRepository, ILogger<HomeController> logger) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        private readonly IUserBoardGameNightRepository _userGameNightRepository = userBoardGameNightRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            var boardGameNights = _boardGameNightRepository.GetAll();
            ViewBag.BannerUrl = "/banner7.jpg";

            return View(boardGameNights);
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
