using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    public class AccountController : Controller
    {
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
