using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BordspellenApplicatie.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string? EmailAddress { get; set; } = null!;
        [Required]
        [UIHint("Password")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        public string? ReturnUrl { get; set; } = "/";
    }
}
