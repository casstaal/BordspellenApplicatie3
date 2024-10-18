namespace BordspellenApplicatie.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? IdentityId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Email { get; set; } = null!;
        //public DateTime DateOfBirth { get; set; }
        //public GenderEnum Gender { get; set; }
        //public Address Address { get; set; }
        //public string? ImageUrl { get; set; }
        //public bool IsLactoseIntolerant { get; set; }
        //public bool IsVegetarian { get; set; }
        //public bool HasNutAllergy { get; set; }
        //public bool DrinksAlcohol { get; set; }
        public required UserRole Role { get; set; }


        public enum GenderEnum
        {
            Male,
            Female,
            Other
        }
    }
}
