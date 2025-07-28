namespace Tourist.API.Models.DTO
{
    public class AddReviewRequestDto
    {
        public Guid TourId { get; set; }
        public string? UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
