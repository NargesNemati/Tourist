using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Tourist.UI.Models.Dto
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

    }
}
