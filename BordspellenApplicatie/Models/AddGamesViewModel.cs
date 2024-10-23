using BordspellenApplicatie.Domain.Models;

namespace BordspellenApplicatie.Models
{
    public class AddGamesViewModel
    {
        public int id { get; set; }
        public Boardgame game { get; set; }
        public List<Boardgame> boardgames { get; set; } = new List<Boardgame>();
    }
}
