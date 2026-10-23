using BordspellenApplicatie.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Bordspellenapplicatie.WebAPI.Models.Request.Auth
{
    public class RegisterRequest
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Email { get; set; } = null!;
        [Required]
        public required string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public required User.GenderEnum Gender { get; set; }
        [Required]
        public required Address Address { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public required bool IsLactoseIntolerant { get; set; }
        [Required]
        public required bool IsVegetarian { get; set; }
        [Required]
        public required bool HasNutAllergy { get; set; }
        [Required]
        public required bool DrinksAlcohol { get; set; }
        [Required]
        public required UserRole Role { get; set; }
    }
}
