using System.ComponentModel.DataAnnotations;

namespace BordspellenApplicatie.Models
{
    public class ReviewViewModel
    {
        [Required(ErrorMessage = "Vul een titel in.")]
        public string Title { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "De score moet tussen 1 en 5 liggen.")]
        public int Score { get; set; }

        [Required(ErrorMessage = "Vul een reviewtekst in.")]
        public string TextReview { get; set; } = string.Empty;

        public int BoardGameNightId { get; set; }
    }
}