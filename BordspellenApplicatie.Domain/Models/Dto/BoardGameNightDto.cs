using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Models.Dto
{
    internal class BoardGameNightDto
    {
        public int Id { get; set; }
        public User Organizer { get; set; }
        public int OrganizerId { get; set; }
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
        public List<Guid> GamesId { get; set; } = new List<Guid>();
        public IEnumerable<int> RegisteredUsersId { get; set; }

        public List<int> ReviewIds { get; set; }
        public virtual ICollection<int> UserBoardGameNightIds { get; set; }
        public bool IsSignedUp { get; set; }
    }
}
