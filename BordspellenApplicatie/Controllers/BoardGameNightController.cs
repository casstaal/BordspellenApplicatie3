using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    [Authorize]
    public class BoardGameNightController(UserManager<IdentityUser> userManager, IBoardGameNightRepository boardGameNightRepository, IUserRepository userRepository, IUserBoardGameNightRepository userBoardGameNightRepository, IGameRepository gameRepository, ILogger<BoardGameNightController> logger, IHttpClientFactory httpClientFactory) : Controller
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        private readonly IUserBoardGameNightRepository _userBoardGameNightRepository = userBoardGameNightRepository;
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly ILogger<BoardGameNightController> _logger = logger;
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

        private async Task<User?> GetCurrentUserAsync()
        {
            var identityUser = await _userManager.GetUserAsync(User);

            if (identityUser == null)
            {
                return null;
            }

            return await _userRepository
                .GetUserByIdentityIdAsync(identityUser.Id);
        }

        public async Task<IActionResult> Details(int id)
        {
            var gameNightDetails = _boardGameNightRepository.GetById(id).Result;

            if (gameNightDetails == null)
            {
                return NotFound();
            }

            var organizer = gameNightDetails.Organizer;

            var viewModel = new BoardGameNightDetailsViewModel
            {
                Id = gameNightDetails.Id,
                Title = gameNightDetails.Title,
                Description = gameNightDetails.Description,
                Organizer = organizer != null
                    ? $"{organizer.Email}"
                    : "Unknown Organizer",
                DateTime = gameNightDetails.DateTime,
                MaxPlayers = gameNightDetails.MaxPlayers,
                Food = gameNightDetails.Food,
                IsLactoseIntoleranceFriendly = gameNightDetails.IsLactoseIntoleranceFriendly,
                IsNutAllergyFriendly = gameNightDetails.IsNutAllergyFriendly,
                IsNonAlcoholicFriendly = gameNightDetails.IsNonAlcoholicFriendly,
                IsVegetarianFriendly = gameNightDetails.IsVegetarianFriendly,
                IsSignedUp = true,
                UserBoardGameNights = gameNightDetails.UserBoardGameNights,
                Games = gameNightDetails.Games.ToList(),
                RegisteredUsersCount = gameNightDetails.UserBoardGameNights.Count(),
                Is18Plus = gameNightDetails.Is18Plus
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var gameNight = await _boardGameNightRepository.GetById(id);

            if (gameNight == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            if (gameNight.OrganizerId != user.Id)
            {
                return Forbid();
            }

            if (gameNight.UserBoardGameNights.Any())
            {
                TempData["ErrorMessage"] =
                    "Deze bordspellenavond kan niet gewijzigd worden " +
                    "omdat er al spelers zijn ingeschreven.";

                return RedirectToAction("Details", new { id });
            }

            var viewModel = new BoardGameNightDetailsViewModel
            {
                Id = gameNight.Id,
                Title = gameNight.Title,
                Description = gameNight.Description,
                Organizer = gameNight.Organizer.Email,
                DateTime = gameNight.DateTime,
                MaxPlayers = gameNight.MaxPlayers,
                Food = gameNight.Food,
                Address = gameNight.Address,
                Is18Plus = gameNight.Is18Plus,
                IsLactoseIntoleranceFriendly =
                    gameNight.IsLactoseIntoleranceFriendly,
                IsNutAllergyFriendly =
                    gameNight.IsNutAllergyFriendly,
                IsNonAlcoholicFriendly =
                    gameNight.IsNonAlcoholicFriendly,
                IsVegetarianFriendly =
                    gameNight.IsVegetarianFriendly,
                Games = gameNight.Games.ToList(),
                RegisteredUsersCount =
                    gameNight.UserBoardGameNights.Count
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            BoardGameNightDetailsViewModel model)
        {
            var gameNight = await _boardGameNightRepository
                .GetById(model.Id);

            if (gameNight == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            if (gameNight.OrganizerId != user.Id)
            {
                return Forbid();
            }

            if (gameNight.UserBoardGameNights.Any())
            {
                TempData["ErrorMessage"] =
                    "Wijzigen is niet toegestaan omdat er al " +
                    "spelers zijn ingeschreven.";

                return RedirectToAction("Details",
                    new { id = gameNight.Id });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            gameNight.Title = model.Title;
            gameNight.Description = model.Description;
            gameNight.DateTime = model.DateTime;
            gameNight.MaxPlayers = model.MaxPlayers;
            gameNight.Food = model.Food;
            gameNight.Address = model.Address;

            gameNight.IsLactoseIntoleranceFriendly =
                model.IsLactoseIntoleranceFriendly;

            gameNight.IsNutAllergyFriendly =
                model.IsNutAllergyFriendly;

            gameNight.IsNonAlcoholicFriendly =
                model.IsNonAlcoholicFriendly;

            gameNight.IsVegetarianFriendly =
                model.IsVegetarianFriendly;

            gameNight.Is18Plus =
                model.Is18Plus ||
                gameNight.Games.Any(g => g.Is18Plus);

            await _boardGameNightRepository.Update(
                gameNight.Id, gameNight);

            return RedirectToAction("Details", "BoardGameNight", new
            {
                id = gameNight.Id,
                title = gameNight.Title,
                date = gameNight.DateTime.ToString("yyyy-MM-dd")
            });
        }

        public IActionResult Create()
        {
            var model = new BoardGameNightDetailsViewModel()
            {
                //Games = new List<Boardgame>()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardGameNightDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    var fieldName = state.Key;
                    var fieldErrors = state.Value.Errors;

                    foreach (var error in fieldErrors)
                    {
                        Console.WriteLine($"Field: {fieldName}, Error: {error.ErrorMessage}");
                    }
                }

                return View(model);

            }

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            var boardGameNight = new BoardGameNight()
            {
                Title = model.Title,
                Organizer = user,
                OrganizerId = user.Id,
                Description = model.Description,
                DateTime = model.DateTime,
                MaxPlayers = model.MaxPlayers,
                Food = model.Food,
                Address = model.Address,
                Is18Plus = model.Is18Plus,
                IsLactoseIntoleranceFriendly = model.IsLactoseIntoleranceFriendly,
                IsNutAllergyFriendly = model.IsNutAllergyFriendly,
                IsNonAlcoholicFriendly = model.IsNonAlcoholicFriendly,
                IsVegetarianFriendly = model.IsVegetarianFriendly,
            };

            await _boardGameNightRepository.Add(boardGameNight);

            return RedirectToAction("AddGames", "BoardGameNight", new { id = boardGameNight.Id });
        }

        [HttpGet]
        public async Task<IActionResult> AddGames(int id)
        {
            var boardGameNight = await _boardGameNightRepository.GetById(id);

            if (boardGameNight == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            if (boardGameNight.OrganizerId != user.Id)
            {
                return Forbid();
            }

            if (boardGameNight.UserBoardGameNights.Any())
            {
                TempData["ErrorMessage"] =
                    "Je kunt geen spellen meer toevoegen omdat er al spelers zijn ingeschreven.";

                return RedirectToAction("Details", new { id });
            }

            var viewModel = new AddGamesViewModel
            {
                id = id,
                boardgames = boardGameNight.Games
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGames(AddGamesViewModel model)
        {
            var gameNight = await _boardGameNightRepository.GetById(model.id);

            if (gameNight == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            if (gameNight.OrganizerId != user.Id)
            {
                return Forbid();
            }

            if (gameNight.UserBoardGameNights.Any())
            {
                TempData["ErrorMessage"] =
                    "Je kunt geen spellen meer toevoegen omdat er al spelers zijn ingeschreven.";

                return RedirectToAction("Details", new { id = gameNight.Id });
            }

            if (model.game == null)
            {
                return BadRequest("Geen bordspel opgegeven.");
            }

            var game = model.game;

            game.Id = Guid.NewGuid();
            game.BoardGameNightId = gameNight.Id;

            await _gameRepository.Add(game);

            if (game.Is18Plus && !gameNight.Is18Plus)
            {
                await _boardGameNightRepository.SetIs18Plus(gameNight.Id);
            }

            return RedirectToAction("AddGames", new { id = gameNight.Id });
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(int gameNightId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            var gameNight = await _boardGameNightRepository.GetById(gameNightId);

            if (gameNight == null)
            {
                return NotFound();
            }

            if (gameNight.OrganizerId == user.Id)
            {
                TempData["ErrorMessage"] =
                    "Je kunt je niet inschrijven voor je eigen spelavond.";

                return RedirectToAction("Details", new { id = gameNightId });
            }

            var today = DateTime.Today;
            var age = today.Year - user.DateOfBirth.Year;

            if (user.DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            if (gameNight.Is18Plus && age < 18)
            {
                TempData["ErrorMessage"] =
                    "Je moet minimaal 18 jaar zijn om je voor deze spelavond in te schrijven.";

                return RedirectToAction("Details", new { id = gameNightId });
            }

            if (gameNight.UserBoardGameNights.Count >= gameNight.MaxPlayers)
            {
                TempData["ErrorMessage"] =
                    "Deze spelavond zit helaas vol. Inschrijven is niet meer mogelijk.";

                return RedirectToAction("Details", new { id = gameNightId });
            }

            var alreadyRegistered = await _userBoardGameNightRepository
                .IsUserRegisteredForBoardGameNightAsync(gameNightId, user.Id);

            if (alreadyRegistered)
            {
                TempData["ErrorMessage"] =
                    "Je bent al ingeschreven voor deze spelavond.";

                return RedirectToAction("Details", new { id = gameNightId });
            }

            var registeredForDate = await _userBoardGameNightRepository
                .IsUserRegisteredForDateAsync(user.Id, gameNight.DateTime.Date);

            if (registeredForDate)
            {
                TempData["ErrorMessage"] =
                    "Je bent al ingeschreven voor een andere spelavond op deze datum.";

                return RedirectToAction("Details", new { id = gameNightId });
            }

            var registration = new UserBoardGameNight
            {
                BoardGameNightId = gameNightId,
                UserId = user.Id
            };

            await _userBoardGameNightRepository
                .AddUserBoardGameNightAsync(registration);

            TempData["SuccessMessage"] =
                "Je bent succesvol ingeschreven voor de spelavond!";

            return RedirectToAction("Details", new { id = gameNightId });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserGameNight(int gameNightId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            var gameNight = await _boardGameNightRepository.GetById(gameNightId);

            if (gameNight == null)
            {
                return NotFound();
            }

            var isRegistered = await _userBoardGameNightRepository
                .IsUserRegisteredForBoardGameNightAsync(gameNightId, user.Id);

            if (!isRegistered)
            {
                TempData["ErrorMessage"] =
                    "Je bent niet ingeschreven voor deze spelavond.";

                return RedirectToAction("Details", new { id = gameNightId });
            }

            await _userBoardGameNightRepository
                .DeleteUserGameNightAsync(gameNightId, user.Id);

            TempData["SuccessMessage"] =
                "Je bent succesvol uitgeschreven voor deze spelavond.";

            return RedirectToAction("Details", new { id = gameNightId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var gameNight = await _boardGameNightRepository.GetById(id);

            if (gameNight == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            if (gameNight.OrganizerId != user.Id)
            {
                return Forbid();
            }

            if (gameNight.UserBoardGameNights.Any())
            {
                TempData["ErrorMessage"] =
                    "Verwijderen is niet toegestaan omdat er al " +
                    "spelers zijn ingeschreven.";

                return RedirectToAction("Details", new { id });
            }

            await _boardGameNightRepository.Delete(id);

            TempData["SuccessMessage"] =
                "Bordspellenavond succesvol verwijderd.";

            return RedirectToAction("BoardGameNight", "Overview");
        }
    }
}

