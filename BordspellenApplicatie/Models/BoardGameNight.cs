namespace BordspellenApplicatie.Models
{
    public class BoardGameNight
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Is18Plus { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }
        public int Seats { get; set; }
        public string Food { get; set; }
        public string Address { get; set; }
    }
}
