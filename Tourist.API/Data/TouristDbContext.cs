using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tourist.API.Models.Domain;
using Tourist.Models.Domain;

namespace Tourist.API.Data
{
    public class TouristDbContext : IdentityDbContext<ApplicationUser>
    {
        public TouristDbContext(DbContextOptions<TouristDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
