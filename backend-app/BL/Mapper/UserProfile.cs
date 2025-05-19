using AutoMapper;
using BL.Models;
using Domain;
namespace BL.Mapper

{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, UserSafeDTO>().ForMember(dest => dest.OrdersPlaced,
                       opt => opt.MapFrom(src => src.OrderCustomers.Count))
            .ForMember(dest => dest.OrdersFulfilled,
                       opt => opt.MapFrom(src => src.OrderArtisans.Count)); ;
            CreateMap<UserSafeDTO, User>();
            CreateMap<UserDTO, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
