using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;

namespace backend_app.Mapper
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageDTO>()
                 .ForMember(dest => dest.Content, opt => opt.MapFrom<ByteArrayToBase64Resolver>());
            CreateMap<ProductImageDTO, ProductImage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Content, opt => opt.MapFrom<Base64ToByteArrayResolver>());
        }
    }
}
