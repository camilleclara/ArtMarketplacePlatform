using AutoMapper;
using BL.Models;
using Domain;
namespace BL.Mapper

{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDTO>();

            CreateMap<MessageDTO, Message>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore());
        }
    }
}
