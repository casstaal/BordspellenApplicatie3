using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BordspellenApplicatie.Domain.Models;
using static BordspellenApplicatie.Models.User;

namespace BordspellenApplicatie.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [EmailAddress]
        public string EmailAddress { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        [Required]
        [UIHint("Password")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        [Required]
        [UIHint("Password Validation")]
        [PasswordPropertyText]
        public string PasswordConfirmation { get; set; } = null!;
        public string? ReturnUrl { get; set; } = "/";

        public required UserRole Role { get; set; }
    }
}
