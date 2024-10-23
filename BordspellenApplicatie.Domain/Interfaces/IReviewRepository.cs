using BordspellenApplicatie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Interfaces
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> GetAll();
        public Review GetById(Guid id);
        public void Add(Review review);
        public void Delete(Guid id);
        public void Update(Review review);
    }
}
