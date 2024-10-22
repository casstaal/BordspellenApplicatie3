using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Mvc;

namespace BordspellenApplicatie.Controllers
{
    public class BoardGameNightController(IBoardGameNightRepository boardGameNightRepository) : Controller
    {
        private readonly IBoardGameNightRepository _boardGameNightRepository = boardGameNightRepository;
        public IActionResult Details(int id, string title, string date, bool is18Plus)
        {
            //// Create a model based on the passed parameters
            //var gameEvent = new BoardGameNight
            //{
            //    Id = id,
            //    Title = title,
            //    Date = DateTime.Parse(date), // Convert date from string
            //    Is18Plus = is18Plus
            //};

            //var futureEvents = new List<BoardGameNight>
            //{
            //    new BoardGameNight { Id = 1, Title = "Game Night 1", Date = DateTime.Now.AddDays(7), Is18Plus = true },
            //    new BoardGameNight { Id = 2, Title = "Game Night 2", Date = DateTime.Now.AddDays(14), Is18Plus = true },
            //    new BoardGameNight { Id = 3, Title = "Game Night 3", Date = DateTime.Now.AddDays(21), Is18Plus = true },
            //    new BoardGameNight { Id = 4, Title = "Game Night 4", Date = DateTime.Now.AddDays(28), Is18Plus = true },
            //    new BoardGameNight { Id = 5, Title = "Game Night 5", Date = DateTime.Now.AddDays(35), Is18Plus = true },
            //    new BoardGameNight { Id = 6, Title = "Game Night 6", Date = DateTime.Now.AddDays(7), Is18Plus = true },
            //    new BoardGameNight { Id = 7, Title = "Game Night 7", Date = DateTime.Now.AddDays(14), Is18Plus = true },
            //    new BoardGameNight { Id = 8, Title = "Game Night 8", Date = DateTime.Now.AddDays(21), Is18Plus = true },
            //    new BoardGameNight { Id = 9, Title = "Game Night 9", Date = DateTime.Now.AddDays(28), Is18Plus = true },
            //    new BoardGameNight { Id = 10, Title = "Game Night 10", Date = DateTime.Now.AddDays(35), Is18Plus = true }
            //};

            //ViewBag.FutureEvents = futureEvents;

            //// Pass the event data to the view
            //return View(gameEvent);
            //DateTime parsedDate;
            //if (!DateTime.TryParse(date, out parsedDate))
            //{
            //    return BadRequest("Invalid date format.");
            //}

            //var gameEvent = new BoardGameNight
            //{
            //    Id = id,
            //    Title = title,
            //    DateTime = parsedDate,
            //};

            //var futureEvents = new List<BoardGameNight>
            //{
            //    new BoardGameNight { Id = 1, Title = "Game Night 1", DateTime = DateTime.Now.AddDays(7)},
            //    new BoardGameNight { Id = 2, Title = "Game Night 2", DateTime = DateTime.Now.AddDays(14)},
            //    // Add more future events
            //};

            //var viewModel = new BoardGameNightDetailsViewModel
            //{
            //    CurrentEvent = gameEvent,
            //    FutureEvents = futureEvents
            //};

            return View();
        }

        public IActionResult Create()
        {
            var model = new BoardGameNightDetailsViewModel()
            {
                Games = new List<Boardgame>()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BoardGameNightDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var boardGameNight = new BoardGameNight()
                {
                    Organizer = model.Organizer,
                    DateTime = model.DateTime,
                    Address = model.Address,
                    Is18Plus = model.Is18Plus,
                    Title = model.Title,
                    Description = model.Description,
                    MaxPlayers = model.MaxPlayers,
                    Food = model.Food,
                    IsLactoseIntoleranceFriendly = model.IsLactoseIntoleranceFriendly,
                    IsVegetarianFriendly = model.IsVegetarianFriendly,
                    IsNutAllergyFriendly = model.IsNutAllergyFriendly,
                    IsNonAlcoholicFriendly = model.IsNonAlcoholicFriendly,
                    Users = model.Users.ToList(),
                    Games = model.Games.ToList()
                };

                _boardGameNightRepository.Add(boardGameNight);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}

