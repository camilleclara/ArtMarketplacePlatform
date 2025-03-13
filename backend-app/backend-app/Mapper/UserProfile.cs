using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;

namespace backend_app.Mapper
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
