using BordspellenApplicatie.Domain.Models;

namespace Bordspellenapplicatie.WebAPI.Models.DTO
{
    public class BoardgameDto
    {
        public Guid Id { get; set; }
        public int BoardGameNightId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boardgame.GenreEnum Genre { get; set; }
        public bool Is18Plus { get; set; }
        public string? ImageUrl { get; set; }
        public Boardgame.GameType Type { get; set; }
    }
}
