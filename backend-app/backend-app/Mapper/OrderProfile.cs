using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Services.Interfaces;

namespace backend_app.Mapper
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
            //.ForMember(dest => dest.Status, opt =>
            //    opt.MapFrom(src => src.Deliveries
            //        .OrderByDescending(d => d.DeliveryDate)
            //        .FirstOrDefault().DeliStatus));
            CreateMap<OrderDTO, Order>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
