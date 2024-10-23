namespace BordspellenApplicatie.Domain.Models
{
    public class Boardgame
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }  
        public GenreEnum Genre { get; set; }
        public bool Is18Plus { get; set; } 
        public string? ImageUrl { get; set; }
        public GameType Type { get; set; }

        public enum GameType
        {
            Board,
            Card
        }

        public enum GenreEnum 
        {
            Family,
            Pary
        }
    }
}
