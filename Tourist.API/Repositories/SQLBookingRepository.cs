using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourist.API.Data;
using Tourist.Models.Domain;

namespace Tourist.API.Repositories
{
    public class SQLBookingRepository : IBookingRepository
    {
        private readonly TouristDbContext dbcontext;
        public SQLBookingRepository(TouristDbContext dbContext)
        {
            this.dbcontext = dbContext;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await dbcontext.Bookings.Include(b => b.Tour).ToListAsync();
        }
        public async Task<Booking> CreateAsync(Booking booking)
        {
            await dbcontext.Bookings.AddAsync(booking);
            await dbcontext.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await dbcontext.Bookings.Include(b => b.Tour).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Booking>> GetByUserIdAsync(string userId)
        {
            return await dbcontext.Bookings
                .Where(b => b.UserId == userId).Include(b => b.Tour)
                .ToListAsync();
        }


        public async Task<Booking?> DeleteAsync(Guid id)
        {
            var existingBooking = await dbcontext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBooking == null)
            {
                return null;
            }

            dbcontext.Bookings.Remove(existingBooking);
            dbcontext.SaveChanges();

            return existingBooking;
        }
    }
}
