using System.ComponentModel.DataAnnotations;
namespace Tourist.Models.Domain;

public class Tour
{
    [Key]
    public Guid Id { get; set; }

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

    // Navigation Properties
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}

