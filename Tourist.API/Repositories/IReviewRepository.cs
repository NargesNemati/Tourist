using Tourist.Models.Domain;

namespace Tourist.API.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(Guid id);
        Task<List<Review>?> GetByTourIdAsync(Guid id);
        Task<Review> CreateAsync(Review review);
        Task<Review?> UpdateAsync(Guid id, Review review);
        Task<Review?> DeleteAsync(Guid id);
    }
}
