using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    public class ReviewController(UserManager<IdentityUser> userManager, IBoardGameNightRepository boardGameNightRepository, IUserRepository userRepository, IUserBoardGameNightRepository userBoardGameNightRepository, IReviewRepository reviewRepository) : Controller
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        private readonly IUserBoardGameNightRepository _userBoardGameNightRepository = userBoardGameNightRepository;
        private readonly IReviewRepository _reviewRepository = reviewRepository;

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel model)
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

            var identityUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (identityUser == null)
            {
                return Unauthorized();
            }

            var user = _userRepository.GetByIdentityId(identityUser.Id);
            if (user == null)
            {
                return Unauthorized();
            }

            var boardGameNight = _boardGameNightRepository.GetById(model.BoardGameNightId);
            if (boardGameNight == null)
            {
                return NotFound(); 
            }

            var review = new Review
            {
                Title = model.Title,
                Score = model.Score,
                TextReview = model.TextReview,
                Author = user,
                Date = model.Date,
                BoardGameNight = boardGameNight 
            };

            _reviewRepository.Add(review);

            return RedirectToAction("Index", "Home");
        }
    }
}
