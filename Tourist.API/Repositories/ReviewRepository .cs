using Microsoft.EntityFrameworkCore;
using Tourist.API.Data;
using Tourist.Models.Domain;

namespace Tourist.API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TouristDbContext dbContext;

        public ReviewRepository(TouristDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await dbContext.Reviews.Include(r => r.Tour).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(Guid id)
        {
            return await dbContext.Reviews.Include(r => r.Tour).FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<Review>?> GetByTourIdAsync(Guid id)
        {
            return await dbContext.Reviews.Include(r => r.Tour)
                .Where(r => r.TourId == id).ToListAsync();
        }

        public async Task<Review> CreateAsync(Review review)
        {
            await dbContext.Reviews.AddAsync(review);
            await dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> UpdateAsync(Guid id, Review review)
        {
            var existingReview = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (existingReview == null)
                return null;

            existingReview.Rating = review.Rating;
            existingReview.Comment = review.Comment;

            await dbContext.SaveChangesAsync();
            return existingReview;
        }

        public async Task<Review?> DeleteAsync(Guid id)
        {
            var review = await dbContext.Reviews.FindAsync(id);
            if (review == null)
                return null;

            dbContext.Reviews.Remove(review);
            await dbContext.SaveChangesAsync();
            return review;
        }
    }
}
