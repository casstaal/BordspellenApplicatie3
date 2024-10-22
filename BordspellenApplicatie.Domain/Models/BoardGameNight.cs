using System.Collections;

namespace BordspellenApplicatie.Domain.Models
{
    public class BoardGameNight
    {
        public int Id { get; set; }
        public User Organizer { get; set; }
        public DateTime DateTime { get; set; }
        public Address Address { get; set; }
        public bool Is18Plus { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public int MaxPlayers { get; set; }
        public string Food { get; set; }
        public bool IsLactoseIntoleranceFriendly { get; set; }
        public bool IsVegetarianFriendly { get; set; }
        public bool IsNutAllergyFriendly { get; set; }
        public bool IsNonAlcoholicFriendly { get; set; }
        public List<Boardgame> Games { get; set; } = new List<Boardgame>();
        public virtual ICollection<UserBoardGameNight> UserBoardGameNights { get; set; }
        public bool IsSignedUp { get; set; }

    }
}
