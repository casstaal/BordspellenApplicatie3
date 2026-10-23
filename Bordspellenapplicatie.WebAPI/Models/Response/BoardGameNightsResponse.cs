using Bordspellenapplicatie.WebAPI.Models.DTO;

namespace Bordspellenapplicatie.WebAPI.Models.Response
{
    public class BoardGameNightsResponse
    {
        public IEnumerable<BoardGameNightDto> BoardGameNights { get; set; }

    }
}
