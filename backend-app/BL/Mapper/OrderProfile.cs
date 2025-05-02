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
                        .FirstOrDefault()))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
                .ForMember(dest => dest.ArtisanName, opt => opt.MapFrom(src => src.Artisan.FirstName + " " + src.Artisan.LastName))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.ItemOrders.Sum(io => io.Product.Price * io.Quantity)))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ItemOrders.Select(io => new OrderedProductDTO
                {
                    ProductId = io.Product.Id,
                    Name = io.Product.Name,
                    Price = io.Product.Price,
                    Quantity = (int)io.Quantity
                }).ToList()));
            CreateMap<OrderDTO, Order>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
