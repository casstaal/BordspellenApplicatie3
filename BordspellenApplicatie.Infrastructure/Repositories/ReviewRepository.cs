using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public Task<Review> AddAsync(Review Review)
        {
            throw new NotImplementedException();
        }

        public Task<Review> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Review> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Review GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateAsync(Review Review)
        {
            throw new NotImplementedException();
        }
    }
}
