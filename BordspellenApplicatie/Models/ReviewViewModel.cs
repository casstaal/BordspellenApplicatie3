using BordspellenApplicatie.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BordspellenApplicatie.Models
{
    public class ReviewViewModel
    {
        [Required]
        public string Title { get; set; } = null!;
        public int Score { get; set; }
        public required string TextReview { get; set; }
        public User Author { get; set; } = null!;
        public DateOnly Date { get; set; }
    }
}
