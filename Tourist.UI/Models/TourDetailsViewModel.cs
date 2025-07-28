using Tourist.UI.Models.Dto;

namespace Tourist.UI.Models
{
    public class TourDetailsViewModel
    {
        public TourDto Tour { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }

}
