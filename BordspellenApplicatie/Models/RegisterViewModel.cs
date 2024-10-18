using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BordspellenApplicatie.Domain.Models;
using static BordspellenApplicatie.Domain.Models.User;

namespace BordspellenApplicatie.Models
{
    public class RegisterViewModel
    {
        //[Required]
        //public string FirstName { get; set; } = null!;
        //[Required]
        //public string LastName { get; set; } = null!;
        //[EmailAddress]
        //public string? Email { get; set; }
        ////public DateTime DateOfBirth { get; set; }
        ////public GenderEnum Gender { get; set; }
        //[Required]
        //[UIHint("Password")]
        //[PasswordPropertyText]
        //public string Password { get; set; } = null!;
        //[Required]
        //[UIHint("Password Validation")]
        //[PasswordPropertyText]
        //public string PasswordConfirmation { get; set; } = null!;
        public string? EmailAddress { get; set; } = null!;
        [Required]
        [UIHint("Password")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        [Required]
        [UIHint("Password Validation")]
        [PasswordPropertyText]
        public string PasswordConfirmation { get; set; } = null!;

        //public Address Address { get; set; } = null!;

        public string? ReturnUrl { get; set; } = "/";

        //public bool IsLactoseIntolerant { get; set; }
        //public bool IsVegetarian { get; set; }
        //public bool HasNutAllergy { get; set; }
        //public bool DrinksAlcohol { get; set; }
        public required UserRole Role { get; set; }
    }
}
