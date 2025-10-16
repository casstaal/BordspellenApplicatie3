//using BordspellenApplicatie.Domain.Interfaces;
//using BordspellenApplicatie.Domain.Models;
//using BordspellenApplicatie.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace BordspellenApplicatie.Controllers
//{
//    [Authorize]
//    public class BoardGameNightController(UserManager<IdentityUser> userManager, IBoardGameNightRepository boardGameNightRepository, IUserRepository userRepository, IUserBoardGameNightRepository userBoardGameNightRepository, ILogger<BoardGameNightController> logger) : Controller
//    {

//        private readonly IUserRepository _userRepository = userRepository;
//        private readonly UserManager<IdentityUser> _userManager = userManager;
//        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
//        private readonly IUserBoardGameNightRepository _userBoardGameNightRepository = userBoardGameNightRepository;
//        private readonly ILogger<BoardGameNightController> _logger = logger;
//        public IActionResult Details(int id)
//        {
//            var gameNightDetails = _boardGameNightRepository.GetById(id);

//            if (gameNightDetails == null)
//            {
//                return NotFound();
//            }

//            var organizer = gameNightDetails.Organizer;


//            var viewModel = new BoardGameNightDetailsViewModel
//            {
//                Id = gameNightDetails.Id,
//                Title = gameNightDetails.Title,
//                Description = gameNightDetails.Description,
//                Organizer = organizer != null
//                    ? $"{organizer.Email}"
//                    : "Unknown Organizer", 
//                DateTime = gameNightDetails.DateTime,
//                MaxPlayers = gameNightDetails.MaxPlayers,
//                Food = gameNightDetails.Food,
//                IsLactoseIntoleranceFriendly = gameNightDetails.IsLactoseIntoleranceFriendly,
//                IsNutAllergyFriendly = gameNightDetails.IsNutAllergyFriendly,
//                IsNonAlcoholicFriendly = gameNightDetails.IsNonAlcoholicFriendly,
//                IsVegetarianFriendly = gameNightDetails.IsVegetarianFriendly,
//                IsSignedUp = true,  
//                UserBoardGameNights = gameNightDetails.UserBoardGameNights ?? new List<UserBoardGameNight>(),
//                Games = gameNightDetails.Games.ToList() ?? new List<Boardgame>(),
//                RegisteredUsersCount = gameNightDetails.RegisteredUsers.Count(),
//                Is18Plus = gameNightDetails.Is18Plus
//            };

//            return View(viewModel);
//        }
//        public IActionResult Edit(int id)
//        {
//            var gameNightDetails = _boardGameNightRepository.GetById(id);

//            if (gameNightDetails == null)
//            {
//                return NotFound();
//            }

//            var organizer = gameNightDetails.Organizer;


//            var viewModel = new BoardGameNightDetailsViewModel
//            {
//                Id = gameNightDetails.Id,
//                Title = gameNightDetails.Title,
//                Description = gameNightDetails.Description,
//                Organizer = organizer != null
//                    ? $"{organizer.Email}"
//                    : "Unknown Organizer",
//                DateTime = gameNightDetails.DateTime,
//                MaxPlayers = gameNightDetails.MaxPlayers,
//                Food = gameNightDetails.Food,
//                Address = gameNightDetails.Address,
//                IsLactoseIntoleranceFriendly = gameNightDetails.IsLactoseIntoleranceFriendly,
//                IsNutAllergyFriendly = gameNightDetails.IsNutAllergyFriendly,
//                IsNonAlcoholicFriendly = gameNightDetails.IsNonAlcoholicFriendly,
//                IsVegetarianFriendly = gameNightDetails.IsVegetarianFriendly,
//                IsSignedUp = true, 
//                Games = gameNightDetails.Games.ToList() ?? new List<Boardgame>(),
//                RegisteredUsersCount = gameNightDetails.RegisteredUsers.Count(), 
//                Is18Plus = gameNightDetails.Is18Plus
//            };

//            return View(viewModel);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(BoardGameNightDetailsViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                _logger.LogWarning("Model is invalid. Model data: {Model}", model);

//                foreach (var key in ModelState.Keys)
//                {
//                    var errors = ModelState[key].Errors;
//                    foreach (var error in errors)
//                    {
//                        _logger.LogWarning("Validation error for {Key}: {Error}", key, error.ErrorMessage);
//                    }
//                }

//                return View("index", "Home"); 
//            }

//            var gameNight = boardGameNightRepository.GetById(model.Id);
//            if (gameNight == null)
//            {
//                return NotFound();
//            }

//            gameNight.Title = model.Title;
//            gameNight.Description = model.Description;
//            gameNight.DateTime = model.DateTime;
//            gameNight.MaxPlayers = model.MaxPlayers;
//            gameNight.Food = model.Food;
//            gameNight.Address = model.Address;
//            gameNight.IsLactoseIntoleranceFriendly = model.IsLactoseIntoleranceFriendly;
//            gameNight.IsNutAllergyFriendly = model.IsNutAllergyFriendly;
//            gameNight.IsNonAlcoholicFriendly = model.IsNonAlcoholicFriendly;
//            gameNight.IsVegetarianFriendly = model.IsVegetarianFriendly;
//            gameNight.Is18Plus = model.Is18Plus;

//            boardGameNightRepository.Update(gameNight);

//            return RedirectToAction("Details", "BoardGameNight", new
//            {
//                id = gameNight.Id,
//                title = gameNight.Title,
//                date = gameNight.DateTime.ToString("yyyy-MM-dd")
//            });
//        }

//        public IActionResult Create()
//        {
//            var model = new BoardGameNightDetailsViewModel()
//            {
//                //Games = new List<Boardgame>()
//            };
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(BoardGameNightDetailsViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                foreach (var state in ModelState)
//                {
//                    var fieldName = state.Key;
//                    var fieldErrors = state.Value.Errors; 

//                    foreach (var error in fieldErrors)
//                    {
//                        Console.WriteLine($"Field: {fieldName}, Error: {error.ErrorMessage}");
//                    }
//                }

//                return View(model);

//            }

//            var identityUser = await userManager.FindByNameAsync(User.Identity.Name);
//            if (identityUser == null)
//            {
//                return Unauthorized();
//            }
//            var user = _userRepository.GetByIdentityId(identityUser.Id);

//            if (user == null)
//            {
//                return Unauthorized();
//            }

//            var boardGameNight = new BoardGameNight()
//            {
//                Title = model.Title,
//                Organizer = user,
//                Description = model.Description,
//                DateTime = model.DateTime,
//                MaxPlayers = model.MaxPlayers,
//                Food = model.Food,
//                Address = model.Address,
//                Is18Plus = model.Is18Plus,
//                IsLactoseIntoleranceFriendly = model.IsLactoseIntoleranceFriendly,
//                IsNutAllergyFriendly = model.IsNutAllergyFriendly,
//                IsNonAlcoholicFriendly = model.IsNonAlcoholicFriendly,
//                IsVegetarianFriendly = model.IsVegetarianFriendly,
//            };

//            _boardGameNightRepository.Add(boardGameNight);

//            return RedirectToAction("AddGames", "BoardGameNight", new { id = boardGameNight.Id });
//        }

//        public IActionResult AddGames(int id)
//        {
//            var boardGameNight = _boardGameNightRepository.GetById(id);
//            if (boardGameNight == null)
//            {
//                return NotFound();
//            }

//            AddGamesViewModel addGamesViewModel = new AddGamesViewModel
//            {
//                id = id,
//                boardgames = boardGameNight.Games
//            };

//            return View(addGamesViewModel);
//        }

//        [HttpPost]
//        public IActionResult AddGames(AddGamesViewModel addGamesViewModel)
//        {
//            var gameNight = boardGameNightRepository.GetById(addGamesViewModel.id);
//            addGamesViewModel.game.Id = new Guid();
//            gameNight.Games.Add(addGamesViewModel.game);
//            _boardGameNightRepository.Update(gameNight);
//            return RedirectToAction("AddGames", "BoardGameNight", new { id = addGamesViewModel.id });
//        }

//        [HttpPost]
//        [Authorize]
//        public async Task<IActionResult> SignUp(int gameNightId)
//        {
//            var identityUser = await _userManager.GetUserAsync(User); 
//            if (identityUser == null)
//            {
//                return Unauthorized();
//            }

//            var user = await _userRepository.GetUserByIdentityIdAsync(identityUser.Id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            var gameNight = _boardGameNightRepository.GetById(gameNightId);
//            if (gameNight == null)
//            {
//                return NotFound();
//            }

//            var today = DateTime.Today;
//            var age = today.Year - user.DateOfBirth.Year;
//            if (user.DateOfBirth.Date > today.AddYears(-age)) age--;

//            if (gameNight.Is18Plus && age < 18)
//            {
//                TempData["ErrorMessage"] = "Je moet 18 jaar of ouder zijn om je in te schrijven voor deze avond.";
//                return RedirectToAction("Details", new { id = gameNightId });
//            }

//            if (gameNight.Organizer != null && gameNight.Organizer.Id == user.Id)
//            {
//                TempData["ErrorMessage"] = "Je kan niet inschrijven voor een door jou georganiseerde avond.";
//                return RedirectToAction("Details", new { id = gameNightId });
//            }

//            var existingRegistration = await _userBoardGameNightRepository.IsUserRegisteredForBoardGameNightAsync(gameNightId, user.Id);

//            if (existingRegistration)
//            {
//                ModelState.AddModelError(string.Empty, "You are already signed up for this game night.");
//                return RedirectToAction("Details", "BoardGameNight", new { id = gameNightId });
//            }

//            var userGameNight = new UserBoardGameNight
//            {
//                BoardGameNightId = gameNightId,
//                UserId = user.Id 
//            };

//            await _userBoardGameNightRepository.AddUserBoardGameNightAsync(userGameNight);

//            TempData["SuccessMessage"] = "Je bent succesvol ingeschreven voor deze avond!";
//            return RedirectToAction("Details", "BoardGameNight", new { id = gameNightId });
//        }

//        [HttpPost]
//        public async Task<IActionResult> DeleteUserGameNight(int gameNightId)
//        {
//            var userId = _userManager.GetUserId(User);
//            var user = await _userRepository.GetUserByIdentityIdAsync(userId);

//            if (user == null)
//            {
//                return Unauthorized();
//            }


//            var boardGameNight = _boardGameNightRepository.GetById(gameNightId);

//            await _userBoardGameNightRepository.DeleteUserGameNightAsync(gameNightId, user.Id);

//            return RedirectToAction("BoardGameNight", "Overview");
//        }
//    }
//}

