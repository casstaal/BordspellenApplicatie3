using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    public class OverviewController : Controller
    {
        public IActionResult BoardGameNight()
        {
            return View();
        }
    }
}
