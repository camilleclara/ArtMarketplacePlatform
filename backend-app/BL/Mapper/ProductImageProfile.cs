using AutoMapper;
using BL.Models;
using Domain;
namespace BL.Mapper

{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageDTO>()
                 .ForMember(dest => dest.Content, opt => opt.MapFrom<ByteArrayToBase64Resolver>());
            CreateMap<ProductImageDTO, ProductImage>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom<Base64ToByteArrayResolver>());
        }
    }
}
