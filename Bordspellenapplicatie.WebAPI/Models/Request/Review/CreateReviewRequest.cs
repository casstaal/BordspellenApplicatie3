using System.ComponentModel.DataAnnotations;

namespace Bordspellenapplicatie.WebAPI.Models.Request.Review
{
    public class CreateReviewRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        [MaxLength(500)]
        public required string TextReview { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int BoardGameNightId { get; set; }
    }
}
