using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public required string TextReview { get; set; }
        public User Author { get; set; }
        public DateOnly Date {  get; set; }
    }
}
