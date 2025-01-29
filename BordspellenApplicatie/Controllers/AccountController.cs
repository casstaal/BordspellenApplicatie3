using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Infrastructure.Repositories;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;
using System.Security.Claims;

namespace BordspellenApplicatie.Controllers
{
    public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository, IBoardGameNightRepository boardGameNightRepository, IUserBoardGameNightRepository userBoardGameNightRepository) : Controller
    {

        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly SignInManager<IdentityUser> signInManager = signInManager;
        private readonly RoleManager<IdentityRole> roleManager = roleManager;
        private readonly IUserRepository userRepository = userRepository;
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        private readonly IUserBoardGameNightRepository _userBoardGameNightRepository = userBoardGameNightRepository;


        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel() { Role = UserRole.User };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerVM)
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "Already signed in.");
            }

            var role = registerVM.Role.ToString();
            if (!roleManager.RoleExistsAsync(role).Result)
            {
                ModelState.AddModelError("", "Specified role does not exist");
            }
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

                return View();
            }

            var user = new IdentityUser()
            {
                UserName = registerVM.EmailAddress,
                Email = registerVM.EmailAddress
            };

            var registerResult = await userManager.CreateAsync(user, registerVM.Password);

            if (registerResult.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim("UserType", "user"));
                await userManager.AddToRoleAsync(user, registerVM.Role.ToString());

                User appUser = new()
                {
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Email = registerVM.EmailAddress,
                    DateOfBirth = registerVM.DateOfBirth,
                    Gender = registerVM.Gender,
                    Address = registerVM.Address,
                    IsLactoseIntolerant = registerVM.IsLactoseIntolerant,
                    IsVegetarian = registerVM.IsVegetarian,
                    HasNutAllergy = registerVM.HasNutAllergy,
                    DrinksAlcohol = registerVM.DrinksAlcohol,
                    Role = registerVM.Role,
                    IdentityId = user.Id
                };
                userRepository.Add(appUser);

                return RedirectToAction(nameof(Login), new { returnUrl = registerVM.ReturnUrl ?? "/" });
            }

            foreach (var error in registerResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl, });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByNameAsync(loginVM.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();


                    if ((await signInManager.PasswordSignInAsync(user,
                        loginVM.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginVM.ReturnUrl);
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View();
        }

        public async Task<IActionResult> DetailsAsync()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var identityUser = await userManager.FindByNameAsync(User?.Identity?.Name ?? "");
                if (identityUser != null)
                {

                    var appUser = userRepository.GetByIdentityId(identityUser.Id);

                    if (appUser != null)
                    {

                        LoginViewModel loginVM = new()
                        {
                            Name = identityUser.UserName ?? "onbekend",
                            EmailAddress = identityUser.Email,
                        };
                        return View(appUser);
                    }
                }
            }

            return RedirectToAction("Login", "Account");
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
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

        public async Task<IActionResult> Registered()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = userManager.GetUserId(User);
            var user = await userRepository.GetUserByIdentityIdAsync(userId);

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

        public async Task<IActionResult> Organized()
        {
            var identityUser = await userManager.FindByNameAsync(User.Identity.Name);
            if (identityUser == null)
            {
                return Unauthorized();
            }

            var organizedGameNights = userRepository.getOrganizedBoardGameNights(identityUser.Id);
            return View(organizedGameNights); 
        }

        public async Task<IActionResult> AllergiesAsync()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var identityUser = await userManager.FindByNameAsync(User?.Identity?.Name ?? "");
                if (identityUser != null)
                {
                    var appUser = userRepository.GetByIdentityId(identityUser.Id);

                    if (appUser != null)
                    {
                        return View(appUser);
                    }
                }
            }

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Settings()
        {
            return View(); 
        }


        [HttpPost]
        public IActionResult UpdateDetails(User model)
        {
            var userBoardgameNights = _userBoardGameNightRepository.GetAll(model.Id);

            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            var userToUpdate = userRepository.GetById(model.Id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FirstName = model.FirstName;
            userToUpdate.LastName = model.LastName;
            userToUpdate.DateOfBirth = model.DateOfBirth;
            userToUpdate.Address.Street = model.Address.Street; 
            userToUpdate.Address.Number = model.Address.Number;
            userToUpdate.Address.City = model.Address.City;
            userToUpdate.UserBoardGameNights = userBoardgameNights.ToList();

            userRepository.UpdateAsync(userToUpdate);

            return RedirectToAction("Details", "Account");
        }


    }
}
