using Bordspellenapplicatie.WebAPI.Models.DTO;

namespace Bordspellenapplicatie.WebAPI.Models.Response
{
    public class ReviewsResponse
    {
        public IEnumerable<ReviewDto> Reviews { get; set; }

    }
}
