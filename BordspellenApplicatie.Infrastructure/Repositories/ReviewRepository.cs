using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Infrastructure.Repositories
{
    public class ReviewRepository(BordspellenavondDBContext dBContext) : IReviewRepository
    {
        public void Add(Review review)
        {
            dBContext.Add(review);
            dBContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public Review GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
