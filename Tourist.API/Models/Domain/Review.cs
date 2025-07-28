using System.ComponentModel.DataAnnotations;
using Tourist.Models.Domain;

namespace Tourist.Models.Domain
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TourId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Tour Tour { get; set; }
    }
}
