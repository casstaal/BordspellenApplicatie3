using Bordspellenapplicatie.WebAPI.Models.DTO;

namespace Bordspellenapplicatie.WebAPI.Models.Response.BoardGame
{
    public class BoardgamesResponse
    {
        public IEnumerable<BoardgameDto> Boardgames { get; set; }
    }
}
