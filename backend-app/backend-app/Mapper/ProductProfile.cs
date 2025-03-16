using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;

namespace backend_app.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsAvailable ?? true)); // Définir IsActive à true si null
        }
    }
}
