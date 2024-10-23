using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    public class OverviewController(UserManager<IdentityUser> userManager, IBoardGameNightRepository boardGameNightRepository, IUserBoardGameNightRepository userBoardGameNightRepository, IUserRepository userRepository) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        private readonly IUserBoardGameNightRepository _userGameNightRepository = userBoardGameNightRepository;
        private readonly IUserRepository _userRepository = userRepository;


        public IActionResult BoardGameNight()
        {
            var boardGameNights = _boardGameNightRepository.GetAll();
            return View(boardGameNights);
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
