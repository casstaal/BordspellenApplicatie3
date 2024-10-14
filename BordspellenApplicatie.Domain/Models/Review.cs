using BordspellenApplicatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Models
{
    internal class Review
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string TextReview { get; set; }
        public User Author { get; set; }
        public DateOnly Date {  get; set; }
    }
}
