using System.ComponentModel.DataAnnotations;

namespace Tourist.UI.Models.Dto
{
    public class AddBookingDto
    {
        public Guid TourId { get; set; }

        //[Required]
        public string? UserId { get; set; }

        public DateTime BookingDate { get; set; }

        public int NumberOfPeople { get; set; }
    }
}
