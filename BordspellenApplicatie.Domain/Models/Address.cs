using Microsoft.EntityFrameworkCore;

namespace BordspellenApplicatie.Domain.Models
{
    [Owned]
    public class Address
    {
        public required string Street { get; set; }
        public required string Number { get; set; }
        public required string City { get; set; }
    }
}
