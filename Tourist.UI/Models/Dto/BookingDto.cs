namespace Tourist.UI.Models.Dto
{
    public class BookingDto
    {

        public Guid Id { get; set; }

        public Guid TourId { get; set; }

        public string UserId { get; set; }

        public DateTime BookingDate { get; set; }

        public int NumberOfPeople { get; set; }

        public string TourDescription { get; set; }
    }
}
