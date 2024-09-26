namespace BordspellenApplicatie.Models
{
    public class Boardgame
    {
        public string? Name { get; set; }
        public int TimeInMinutes { get; set; }
        public string? Genre { get; set; }
        public string? GameType { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string? Description { get; set; }
    }
}
