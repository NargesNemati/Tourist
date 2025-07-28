using AutoMapper;
using NZWalks.API.Models.DTO;
using Tourist.API.Models.DTO;
using Tourist.Models.Domain;
using Tourist.Models.DTO;
namespace Tourist.Mappings
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Tour, TourDto>().ReverseMap();
            CreateMap<AddRequestToursDto, Tour>().ReverseMap();
            CreateMap<UpdateTourRequestDto, Tour>().ReverseMap();
            CreateMap<AddBookingRequestDto, Booking>().ReverseMap();
            //CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.TourDescription,
                 opt => opt.MapFrom(src => src.Tour.Description));

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<AddReviewRequestDto, Review>().ReverseMap();
            CreateMap<UpdateReviewRequestDto, Review>().ReverseMap();
        }
    }
}
