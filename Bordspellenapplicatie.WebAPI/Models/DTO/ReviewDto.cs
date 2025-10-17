using BordspellenApplicatie.Domain.Models;

namespace Bordspellenapplicatie.WebAPI.Models.DTO
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }
        public required string TextReview { get; set; }
        public int AuthorId { get; set; }
        public DateTime Date { get; set; }
        public int BoardGameNightId { get; set; }
    }
}
