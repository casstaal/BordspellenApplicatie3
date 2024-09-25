using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    public class BoardGameNightController : Controller
    {
        public IActionResult Details(int id, string title, string date, bool is18Plus)
        {
            // Create a model based on the passed parameters
            var gameEvent = new BoardGameNight
            {
                Id = id,
                Title = title,
                Date = DateTime.Parse(date), // Convert date from string
                Is18Plus = is18Plus
            };

            // Pass the event data to the view
            return View(gameEvent);
        }
    }
}

