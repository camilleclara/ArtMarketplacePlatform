using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;

namespace backend_app.Mapper
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
