using System.ComponentModel.DataAnnotations;

namespace Bordspellenapplicatie.WebAPI.Models.Request.Review
{
    public class UpdateReviewRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        [MaxLength(500)]
        public required string TextReview { get; set; }
    }
}
