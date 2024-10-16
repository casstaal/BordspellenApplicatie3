using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Models;
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
        public IQueryable<Review> GetAllAsync();

        public Review GetById(Guid id);
        public Task<Review> GetByIdAsync(Guid id);

        public Task<Review> AddAsync(Review Review);
        public Task<Review> UpdateAsync(Review Review);
        public Task<Review> DeleteAsync(int id);
    }
}
