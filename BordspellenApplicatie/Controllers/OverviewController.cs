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
        private readonly IUserBoardGameNightRepository _userBoardGameNightRepository = userBoardGameNightRepository;
        private readonly IUserRepository _userRepository = userRepository;


        public IActionResult BoardGameNight()
        {
            var boardGameNights = _boardGameNightRepository.GetAll();
            return View(boardGameNights);
        }

        public async Task<IActionResult> OrganizedBoardGameNights()
        {
            var identityUser = await userManager.FindByNameAsync(User.Identity.Name);
            if (identityUser == null)
            {
                return Unauthorized();
            }

            var organizedGameNights = userRepository.getOrganizedBoardGameNights(identityUser.Id);
            return View(organizedGameNights);
        }

        private BoardGameNightDetailsViewModel MapToViewModel(UserBoardGameNight userBoardGameNight)
        {
            if (userBoardGameNight.GameNight == null)
            {
                throw new ArgumentNullException(nameof(userBoardGameNight.GameNight), "GameNight cannot be null.");
            }

            var organizer = userBoardGameNight.GameNight.Organizer;

            return new BoardGameNightDetailsViewModel
            {
                Id = userBoardGameNight.GameNight.Id,
                Title = userBoardGameNight.GameNight.Title ?? "No Title",
                Organizer = organizer != null
                    ? $"{organizer.FirstName} {organizer.LastName}"
                    : "Unknown Organizer",
                Description = userBoardGameNight.GameNight.Description ?? "No Description",
                DateTime = userBoardGameNight.GameNight.DateTime,
                MaxPlayers = userBoardGameNight.GameNight.MaxPlayers,
                Food = userBoardGameNight.GameNight.Food ?? "No Food Information",
                Is18Plus = userBoardGameNight.GameNight.Is18Plus,
                IsLactoseIntoleranceFriendly = userBoardGameNight.GameNight.IsLactoseIntoleranceFriendly,
                IsNutAllergyFriendly = userBoardGameNight.GameNight.IsNutAllergyFriendly,
                IsNonAlcoholicFriendly = userBoardGameNight.GameNight.IsNonAlcoholicFriendly,
                IsVegetarianFriendly = userBoardGameNight.GameNight.IsVegetarianFriendly,
                IsSignedUp = true,
                Games = userBoardGameNight.GameNight.Games?.ToList() ?? new List<Boardgame>(),
                RegisteredUsersCount = userBoardGameNight.GameNight.RegisteredUsers?.Count ?? 0
            };
        }

        public async Task<IActionResult> RegisteredBoardGameNights()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = _userManager.GetUserId(User);
            var user = await _userRepository.GetUserByIdentityIdAsync(userId);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userGameNights = _userBoardGameNightRepository.GetAll(user.Id);

            if (userGameNights == null || !userGameNights.Any())
            {
                ViewBag.Message = "You are not signed up for any game nights yet.";
                return View(Enumerable.Empty<BoardGameNightDetailsViewModel>());
            }

            var viewModel = userGameNights.Select(MapToViewModel).ToList();

            return View(viewModel);
        }

        public IActionResult Boardgame()
        {
            return View();
        }
    }
}
