using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BordspellenApplicatie.Domain.Models;

namespace BordspellenApplicatie.Models
{
    public class BoardGameNightDetailsViewModel
    {
        //public BoardGameNight CurrentEvent { get; set; }
        //public List<BoardGameNight> FutureEvents { get; set; }

        public User Organizer { get; set; } = null!;
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
        public IEnumerable<User> Users { get; set; } = null!;
        public IEnumerable<Boardgame> Games { get; set; } = null!;
    }
}
