using System.ComponentModel.DataAnnotations;

namespace Tourist.API.Models.DTO
{
    public class UpdateTourRequestDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Destination { get; set; }

        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
