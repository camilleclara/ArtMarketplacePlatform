using AutoMapper;
using BL.Models;
using Domain;
namespace BL.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.ActiveDelivery, opt => opt.MapFrom(src =>
                    src.Deliveries
                        .Where(d => d.IsActive == true) // Null est considéré comme actif
                        .FirstOrDefault()));
            CreateMap<OrderDTO, Order>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
