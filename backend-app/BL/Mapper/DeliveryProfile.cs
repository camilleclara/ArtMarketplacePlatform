

using AutoMapper;
using BL.Models;
using Domain;

namespace BL.Mapper
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Delivery, DeliveryDTO>();
            CreateMap<DeliveryDTO, Delivery>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
