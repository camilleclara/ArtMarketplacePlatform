using AutoMapper;
using BL.Models;
using Domain;
namespace BL.Mapper

{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDTO>();

            CreateMap<ReviewDTO, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
