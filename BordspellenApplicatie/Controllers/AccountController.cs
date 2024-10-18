using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BordspellenApplicatie.Controllers
{
    public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository) : Controller
    {

        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly SignInManager<IdentityUser> signInManager = signInManager;
        private readonly RoleManager<IdentityRole> roleManager = roleManager;
        private readonly IUserRepository userRepository = userRepository;


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
                //foreach (var error in ModelState)
                //{
                //    Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                //}
                //return View(registerVM); // Pass the model back to the view for repopulation
                //var errors = ModelState.Values.SelectMany(v => v.Errors);

                foreach (var state in ModelState)
                {
                    var fieldName = state.Key; // The field (e.g., "Email", "Password")
                    var fieldErrors = state.Value.Errors; // The errors associated with that field

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
                // Een rol is een claim. Claims zijn algemener, rollen zijn iets makkelijker te gebruiken -->
                await userManager.AddClaimAsync(user, new Claim("UserType", "user"));
                await userManager.AddToRoleAsync(user, registerVM.Role.ToString());

                //BordspellenApplicatie.Domain.Models.User appUser = new BordspellenApplicatie.Domain.Models.User()
                User appUser = new()
                {
                    //FirstName = registerVM.FirstName,
                    //LastName = registerVM.LastName,
                    Email = registerVM.EmailAddress,
                    //DateOfBirth = registerVM.DateOfBirth,
                    //Gender = registerVM.Gender,
                    //Address = registerVM.Address,
                    //IsLactoseIntolerant = registerVM.IsLactoseIntolerant,
                    //IsVegetarian = registerVM.IsVegetarian,
                    //HasNutAllergy = registerVM.HasNutAllergy,
                    //DrinksAlcohol = registerVM.DrinksAlcohol,
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
                        return RedirectToAction(nameof(Details), loginVM);
                        //return RedirectToAction(nameof(Details));
                        //return Redirect(loginVM?.ReturnUrl ?? "/Guest/List");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View();
        }

        public IActionResult Details()
        {
            return View(); 
        }

        public IActionResult Registered()
        {
            return View(); 
        }

        public IActionResult Organized()
        {
            return View(); 
        }

        public IActionResult Allergies()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View(); 
        }



    }
}
