using BordspellenApplicatie.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Bordspellenapplicatie.WebAPI.Models.Request
{
    public class CreateBoardGameNight
    {
        [Required]
        public int OrganizerId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(2, 100)]
        public int MaxPlayers { get; set; }

        public bool Is18Plus { get; set; }

        public string? Food { get; set; }

        public bool IsLactoseIntoleranceFriendly { get; set; }
        public bool IsVegetarianFriendly { get; set; }
        public bool IsNutAllergyFriendly { get; set; }
        public bool IsNonAlcoholicFriendly { get; set; }

        public List<int> BoardGameIds { get; set; } = new List<int>();
    }
}
