namespace BordspellenApplicatie.Models
{
    public class BoardGameNight
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Is18Plus { get; set; }
        public string ImageUrl { get; set; }
    }
}
