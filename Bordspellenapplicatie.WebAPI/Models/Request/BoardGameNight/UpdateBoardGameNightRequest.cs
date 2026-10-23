using BordspellenApplicatie.Domain.Models;

namespace Bordspellenapplicatie.WebAPI.Models.Request.BoardGameNight
{
    public class UpdateBoardGameNightRequest
    {
        public DateTime DateTime { get; set; }
        public Address Address { get; set; }
        public bool Is18Plus { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int MaxPlayers { get; set; }
        public string Food { get; set; }

        public bool IsLactoseIntoleranceFriendly { get; set; }
        public bool IsVegetarianFriendly { get; set; }
        public bool IsNutAllergyFriendly { get; set; }
        public bool IsNonAlcoholicFriendly { get; set; }

        public List<int>? BoardGameIds { get; set; }
    }
}
