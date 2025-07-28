using Microsoft.EntityFrameworkCore;

using Tourist.API.Data;
using Tourist.Models.Domain;

namespace Tourist.Repositories
{
    public class SQLTourRepository : ITourRepository
    {
        public readonly TouristDbContext dbContext;
        public SQLTourRepository(TouristDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Tour>> GetAllAsync()
        {
            return await dbContext.Tours.ToListAsync();
        }

        public async Task<Tour?> GetByIdAsync(Guid id)
        {
            return await dbContext.Tours.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Tour> CreateAsync(Tour tour)
        {
            await dbContext.Tours.AddAsync(tour);
            await dbContext.SaveChangesAsync();
            return tour;
        }

        public async Task<Tour?> UpdateAsync(Guid id, Tour tour)
        {
            var existingTour = await dbContext.Tours.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTour == null)
            {
                return null;
            }

            existingTour.Name = tour.Name;
            existingTour.Destination = tour.Destination;
            existingTour.Description = tour.Description;
            existingTour.Price = tour.Price;
            existingTour.Capacity = tour.Capacity;
            existingTour.StartDate = tour.StartDate;
            existingTour.EndDate = tour.EndDate;

            await dbContext.SaveChangesAsync();

            return existingTour;
        }

        public async Task<Tour?> DeleteAsync(Guid id)
        {
            var existingTour = await dbContext.Tours.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTour == null)
            {
                return null;
            }

            dbContext.Tours.Remove(existingTour);
            await dbContext.SaveChangesAsync();
            return existingTour;
        }

    }
}
