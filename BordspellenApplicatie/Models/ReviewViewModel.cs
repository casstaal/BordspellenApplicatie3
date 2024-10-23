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
        public string Author { get; set; } = null!;
        public DateTime Date { get; set; }

        [Required]
        public int BoardGameNightId { get; set; }
    }
}
