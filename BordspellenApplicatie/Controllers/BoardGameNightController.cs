using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Infrastructure.Repositories;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BordspellenApplicatie.Controllers
{
    [Authorize]
    public class BoardGameNightController(UserManager<IdentityUser> userManager, IBoardGameNightRepository boardGameNightRepository, IUserRepository userRepository, IUserBoardGameNightRepository userBoardGameNightRepository) : Controller
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        private readonly IUserBoardGameNightRepository _userBoardGameNightRepository = userBoardGameNightRepository;

        public IActionResult Details(BoardGameNightDetailsViewModel boardGameNightDetailsViewModel)
        {
            return View(boardGameNightDetailsViewModel);
        }
        //public async Task<IActionResult> Details(BoardGameNightDetailsViewModel boardGameNightDetailsViewModel)
        //{
        //    //// Get the current user's ID (assuming you're using Identity or similar authentication)
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Fetch the logged-in user's ID

        //    //// Fetch the board game night details from your repository (assuming you have this method)
        //    //var gameNightDetails = await _boardGameNightRepository.GetBoardGameNightByIdAsync(boardGameNightId);

        //    //if (gameNightDetails == null)
        //    //{
        //    //    return NotFound();  // Handle the case where the board game night doesn't exist
        //    //}

        //    //// Check if the current user is signed up for this game night
        //    bool isSignedUp = await _userBoardGameNightRepository.IsUserRegisteredForBoardGameNightAsync(boardGameNightDetailsViewModel.Id, userId);

        //    //// Build the view model
        //    //var viewModel = new BoardGameNightDetailsViewModel
        //    //{
        //    //    Id = gameNightDetails.Id,
        //    //    Title = gameNightDetails.Title,
        //    //    Description = gameNightDetails.Description,
        //    //    Organizer = gameNightDetails.Organizer,
        //    //    DateTime = gameNightDetails.DateTime,
        //    //    MaxPlayers = gameNightDetails.MaxPlayers,
        //    //    IsSignedUp = isSignedUp,  // Set this to true if the user is signed up
        //    //    Games = gameNightDetails.Games.ToList(),
        //    //    RegisteredUsersCount = gameNightDetails.RegisteredUsers.Count(),  // Or whatever logic you use to count
        //    //    Is18Plus = gameNightDetails.Is18Plus
        //    //};
        //    var viewModel = new BoardGameNightDetailsViewModel
        //    {
        //        IsSignedUp = isSignedUp
        //    };

        //    return View(viewModel);
        //    //return View(boardGameNightDetailsViewModel);
        //}

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
                    var fieldName = state.Key; // The field (e.g., "Email", "Password")
                    var fieldErrors = state.Value.Errors; // The errors associated with that field

                    foreach (var error in fieldErrors)
                    {
                        Console.WriteLine($"Field: {fieldName}, Error: {error.ErrorMessage}");
                    }
                }

                return View(model);

            }

            var identityUser = await userManager.FindByNameAsync(User.Identity.Name);
            if (identityUser == null)
            {
                return Unauthorized();
            }
            var user = _userRepository.GetByIdentityId(identityUser.Id);

            if (user == null)
            {
                return Unauthorized();
            }

            var boardGameNight = new BoardGameNight()
            {
                Title = model.Title,
                Organizer = user,
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

            _boardGameNightRepository.Add(boardGameNight);

            return RedirectToAction("AddGames", "BoardGameNight", new { id = boardGameNight.Id });
        }

        public IActionResult AddGames(int id)
        {
            var boardGameNight = _boardGameNightRepository.GetById(id);
            if(boardGameNight == null)
            {
                return NotFound();
            }

            AddGamesViewModel addGamesViewModel = new AddGamesViewModel
            {
                id = id,
                boardgames = boardGameNight.Games
            };

            return View(addGamesViewModel);
        }

        [HttpPost]
        public IActionResult AddGames(AddGamesViewModel addGamesViewModel)
        {
            var gameNight = boardGameNightRepository.GetById(addGamesViewModel.id);
            addGamesViewModel.game.Id = new Guid();
            gameNight.Games.Add(addGamesViewModel.game);
            _boardGameNightRepository.Update(gameNight);
            return RedirectToAction("AddGames", "BoardGameNight", new { id = addGamesViewModel.id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignUp(int gameNightId)
        {
            var identityUser = await _userManager.GetUserAsync(User); // Get the IdentityUser
            if (identityUser == null)
            {
                return Unauthorized();
            }

            // Assuming you have a repository or service to retrieve the User model by IdentityUser.Id
            var user = await _userRepository.GetUserByIdentityIdAsync(identityUser.Id);
            if (user == null)
            {
                return NotFound(); // Handle case when User doesn't exist in your own user table
            }

            var gameNight = _boardGameNightRepository.GetById(gameNightId);
            if (gameNight == null)
            {
                return NotFound();
            }

            // Check if the user is already signed up for the game night using the repository method
            var existingRegistration = await _userBoardGameNightRepository.IsUserRegisteredForBoardGameNightAsync(gameNightId, identityUser.Id);

            if (existingRegistration)
            {
                ModelState.AddModelError(string.Empty, "You are already signed up for this game night.");
                return RedirectToAction("Details", "BoardGameNight", new { id = gameNightId });
            }

            var userGameNight = new UserBoardGameNight
            {
                BoardGameNightId = gameNightId,
                UserId = user.Id // This works because user.Id is an int
            };

            // Add the new record to the database using the repository method
            await _userBoardGameNightRepository.AddUserBoardGameNightAsync(userGameNight);

            // Redirect to the event details page or display a success message
            return RedirectToAction("Details", "BoardGameNight", new { id = gameNightId });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserGameNight(int gameNightId)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userRepository.GetUserByIdentityIdAsync(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            await _userBoardGameNightRepository.DeleteUserGameNightAsync(gameNightId, user.Id);
            return Ok();
        }
    }
}

