using System.Collections;

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
        public Person Organizer { get; set; }
        public int Seats { get; set; }
        public string Food { get; set; }
        public Address Address { get; set; }
        //public ArrayList<Boardgame> Boardgames { get; set; }
    }
}
