using AutoMapper;
using BL.Models;
using BL.Models.Statistics;
using Domain;
using System.Linq;

namespace BL.Mapper
{
    public class StatisticsProfile : Profile
    {
        public StatisticsProfile()
        {
            // Mapping for trending products
            CreateMap<Product, TrendingProductDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.ArtisanName, opt => opt.MapFrom(src => src.Artisan != null ? $"{src.Artisan.FirstName} {src.Artisan.LastName}" : "Unknown"))
                .ForMember(dest => dest.OrderCount, opt => opt.MapFrom(src => src.ItemOrders.Count))
                .ForMember(dest => dest.TotalRevenue, opt => opt.MapFrom(src => src.Price.HasValue ? src.ItemOrders.Sum(io => io.Quantity * src.Price.Value) : 0))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Reviews.Any() ? src.Reviews.Average(r => r.Score) : (double?)null))
                .ForMember(dest => dest.SalesCount, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var salesDict = (Dictionary<int, int>)context.Items["SalesCounts"];
                    return salesDict.TryGetValue(src.Id, out var count) ? count : 0;
                }));

        }
    }
}