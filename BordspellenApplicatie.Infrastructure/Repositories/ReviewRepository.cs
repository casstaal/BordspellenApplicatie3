using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BordspellenApplicatie.Infrastructure.Repositories
{
    public class ReviewRepository(BordspellenavondDBContext dBContext) : IReviewRepository
    {
        public async Task<Review> Add(Review review)
        {
            await dBContext.Reviews.AddAsync(review);
            await dBContext.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> Delete(Guid id)
        {
            var existingReview = await dBContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (existingReview == null)
            {
                return null;
            }

            dBContext.Reviews.Remove(existingReview);
            await dBContext.SaveChangesAsync();
            return existingReview;
        }

        public async Task<List<Review>> GetAll()
        {
            return await dBContext.Reviews.ToListAsync();
        }

        public async Task<List<Review>?> GetByAuthorId(int id)
        {
            return await dBContext.Reviews.Where(x => x.AuthorId == id).ToListAsync();
        }

        public async Task<List<Review>?> GetByBoardGameNightId(int id)
        {
            return await dBContext.Reviews.Where(x => x.BoardGameNightId == id).ToListAsync();
        }

        public async Task<Review?> GetById(Guid id)
        {
            return await dBContext.Reviews.FirstOrDefaultAsync(x =>x.Id == id);
        }

        public async Task<Review> Update(Guid id, Review review)
        {
            var existingReview = await dBContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (existingReview == null)
            {
                return null;
            }

            existingReview.Title = review.Title;
            existingReview.Score = review.Score;
            existingReview.TextReview = review.TextReview;

            await dBContext.SaveChangesAsync();

            return existingReview;
        }
    }
}
