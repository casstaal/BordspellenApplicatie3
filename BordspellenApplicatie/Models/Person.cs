namespace BordspellenApplicatie.Models
{
    public class Person
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public bool IsOrganizer { get; set; }
        public string ImageUrl { get; set; }
    }
}
