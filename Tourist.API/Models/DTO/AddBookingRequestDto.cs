using System.ComponentModel.DataAnnotations;
using Tourist.Models.Domain;

namespace Tourist.API.Models.DTO
{
    public class AddBookingRequestDto
    {

        [Required]
        public Guid TourId { get; set; }

        //[Required]
        public string? UserId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfPeople { get; set; }

    }
}
