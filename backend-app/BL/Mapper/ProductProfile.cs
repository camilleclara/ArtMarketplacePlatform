using AutoMapper;
using BL.Models;
using Domain;
namespace BL.Mapper

{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ArtisanName, opt => opt.MapFrom(src => (src.Artisan.FirstName + " " + src.Artisan.LastName)));


            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ?? true))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable ?? true));
        }
    }
}
