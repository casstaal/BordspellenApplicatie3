
using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    [Authorize]
    public class ReviewController(
        UserManager<IdentityUser> userManager,
        IBoardGameNightRepository boardGameNightRepository,
        IUserRepository userRepository,
        IUserBoardGameNightRepository userBoardGameNightRepository,
        IReviewRepository reviewRepository) : Controller
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        private readonly IUserBoardGameNightRepository _userBoardGameNightRepository = userBoardGameNightRepository;
        private readonly IReviewRepository _reviewRepository = reviewRepository;

        private async Task<User?> GetCurrentUserAsync()
        {
            var identityUser = await _userManager.GetUserAsync(User);

            if (identityUser == null)
                return null;

            return await _userRepository
                .GetUserByIdentityIdAsync(identityUser.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
                return Unauthorized();

            var gameNight = await _boardGameNightRepository.GetById(id);

            if (gameNight == null)
                return NotFound();

            if (gameNight.DateTime.Date >= DateTime.Today)
            {
                TempData["ErrorMessage"] =
                    "Je kunt pas een review plaatsen nadat de bordspelavond is afgelopen.";

                return RedirectToAction("Details", "BoardGameNight", new { id });
            }

            var isRegistered = await _userBoardGameNightRepository
                .IsUserRegisteredForBoardGameNightAsync(id, user.Id);

            if (!isRegistered)
            {
                TempData["ErrorMessage"] =
                    "Alleen aangemelde spelers kunnen een review plaatsen.";

                return RedirectToAction("Details", "BoardGameNight", new { id });
            }

            var alreadyReviewed = await _reviewRepository
                .HasUserReviewedGameNightAsync(id, user.Id);

            if (alreadyReviewed)
            {
                TempData["ErrorMessage"] =
                    "Je hebt deze bordspelavond al beoordeeld.";

                return RedirectToAction("Details", "BoardGameNight", new { id });
            }

            var model = new ReviewViewModel
            {
                BoardGameNightId = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel model)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
                return Unauthorized();

            var gameNight = await _boardGameNightRepository
                .GetById(model.BoardGameNightId);

            if (gameNight == null)
                return NotFound();

            if (gameNight.DateTime.Date >= DateTime.Today)
            {
                TempData["ErrorMessage"] =
                    "Je kunt pas een review plaatsen nadat de bordspelavond is afgelopen.";

                return RedirectToAction(
                    "Details",
                    "BoardGameNight",
                    new { id = model.BoardGameNightId });
            }

            var isRegistered = await _userBoardGameNightRepository
                .IsUserRegisteredForBoardGameNightAsync(
                    model.BoardGameNightId, user.Id);

            if (!isRegistered)
            {
                TempData["ErrorMessage"] =
                    "Alleen aangemelde spelers kunnen een review plaatsen.";

                return RedirectToAction(
                    "Details",
                    "BoardGameNight",
                    new { id = model.BoardGameNightId });
            }

            var alreadyReviewed = await _reviewRepository
                .HasUserReviewedGameNightAsync(
                    model.BoardGameNightId, user.Id);

            if (alreadyReviewed)
            {
                TempData["ErrorMessage"] =
                    "Je hebt deze bordspelavond al beoordeeld.";

                return RedirectToAction(
                    "Details",
                    "BoardGameNight",
                    new { id = model.BoardGameNightId });
            }

            if (!ModelState.IsValid)
                return View(model);

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Score = model.Score,
                TextReview = model.TextReview,
                AuthorId = user.Id,
                Date = DateTime.Now,
                BoardGameNightId = gameNight.Id
            };

            await _reviewRepository.Add(review);

            TempData["SuccessMessage"] =
                "Je review is succesvol geplaatst!";

            return RedirectToAction(
                "Details",
                "BoardGameNight",
                new { id = gameNight.Id });
        }
    }
}
