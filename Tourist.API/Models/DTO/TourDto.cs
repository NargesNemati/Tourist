using System.ComponentModel.DataAnnotations;
using Tourist.Models.Domain;

namespace Tourist.Models.DTO
{
    public class TourDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }

        public int Capacity { get; set; }
        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
