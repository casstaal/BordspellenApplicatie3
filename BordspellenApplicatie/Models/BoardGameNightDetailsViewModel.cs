using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BordspellenApplicatie.Domain.Models;

namespace BordspellenApplicatie.Models
{
    public class BoardGameNightDetailsViewModel
    {
        public string Organizer { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public Address Address { get; set; } = null!;
        public bool Is18Plus { get; set; }

        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public int MaxPlayers { get; set; }
        [Required]
        public string Food { get; set; } = null!;
        public bool IsLactoseIntoleranceFriendly { get; set; }
        public bool IsVegetarianFriendly { get; set; }
        public bool IsNutAllergyFriendly { get; set; }
        public bool IsNonAlcoholicFriendly { get; set; }
        public List<Boardgame> Games { get; set; } = new List<Boardgame>();
        // Reviews van deze bordspelavond
        public List<Review> Reviews { get; set; } = new();

        // Reviews en statistieken van de organisator
        public double? OrganizerAverageScore { get; set; }
        public int OrganizerReviewCount { get; set; }
        public int OrganizerGameNightCount { get; set; }

        // Mag de ingelogde speler een review plaatsen?
        public bool CanWriteReview { get; set; }
        public ICollection<UserBoardGameNight> UserBoardGameNights { get; set; } = new List<UserBoardGameNight>();
        public int RegisteredUsersCount { get; set; }
        public bool IsSignedUp { get; set; }
        public int Id { get; set; }

        // Dieetwensen van de ingelogde gebruiker
        public bool IsLactoseIntolerant { get; set; }
        public bool HasNutAllergy { get; set; }
        public bool IsVegetarian { get; set; }
        public bool DrinksAlcohol { get; set; }
    }
}
