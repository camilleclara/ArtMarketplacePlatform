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
            CreateMap<UserDTO, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
