using System.ComponentModel.DataAnnotations;

namespace Bordspellenapplicatie.WebAPI.Models.Request
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
