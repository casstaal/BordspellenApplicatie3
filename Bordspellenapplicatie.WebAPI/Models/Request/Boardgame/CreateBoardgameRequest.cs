using BordspellenApplicatie.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Bordspellenapplicatie.WebAPI.Models.Request.BoardGame
{
    public class CreateBoardgameRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int BoardGameNightId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Boardgame.GenreEnum Genre { get; set; }
        [Required]
        public bool Is18Plus { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public Boardgame.GameType Type { get; set; }
    }
}
