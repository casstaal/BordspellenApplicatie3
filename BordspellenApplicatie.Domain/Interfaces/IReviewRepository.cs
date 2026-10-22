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
        Task<List<Review>> GetAll();
        Task<Review?> GetById(Guid id);
        Task<List<Review>?> GetByBoardGameNightId(int id);
        Task<List<Review>?> GetByAuthorId(int id);
        Task<Review> Add(Review review);
        Task<Review?> Delete(Guid id);
        Task<Review> Update(Guid id, Review review);
        Task<List<Review>> GetByOrganizerId(int organizerId);
        Task<bool> HasUserReviewedGameNightAsync(
            int boardGameNightId, int authorId);
    }
}
