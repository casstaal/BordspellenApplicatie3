using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    public class ReviewController : Controller
    {

        public IActionResult Create()
        {
            return View();
        }
    }
}
