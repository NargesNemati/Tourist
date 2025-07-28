using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using Tourist.Models.Domain;

namespace Tourist.API.Repositories
{
    public interface IBookingRepository
    {
        //Task<List<Booking>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
        //    string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<List<Booking>> GetAllAsync();
        Task<List<Booking>> GetByUserIdAsync(string userId);

        Task<Booking> CreateAsync(Booking booking);
        Task<Booking?> GetByIdAsync(Guid id);
        Task<Booking?> DeleteAsync(Guid id);
    }
}
