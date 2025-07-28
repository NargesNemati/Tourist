namespace Tourist.UI.Models.Dto
{
    public class AddReviewDto
    {
        public Guid TourId { get; set; }
        public string? UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
