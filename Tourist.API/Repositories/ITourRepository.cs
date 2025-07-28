using Tourist.Models;
using Tourist.Models.Domain;
namespace Tourist.Repositories
{
    public interface ITourRepository
    {
        Task<List<Tour>> GetAllAsync();
        Task<Tour?> GetByIdAsync(Guid id);
        Task<Tour> CreateAsync(Tour tour);
        Task<Tour?> UpdateAsync(Guid id, Tour tour);
        Task<Tour?> DeleteAsync(Guid id);
    }
}
