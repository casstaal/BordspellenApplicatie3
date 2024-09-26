namespace BordspellenApplicatie.Models
{
    public class BoardGameNightDetailsViewModel
    {
        public BoardGameNight CurrentEvent { get; set; }
        public List<BoardGameNight> FutureEvents { get; set; }
    }
}
