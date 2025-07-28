using System.ComponentModel.DataAnnotations;
using Tourist.Models.Domain;

namespace Tourist.Models.Domain
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TourId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfPeople { get; set; }

        // Navigation Property
        public Tour Tour { get; set; }
    }
}



