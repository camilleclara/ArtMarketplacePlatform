using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;

namespace backend_app.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>().ForMember(dest => dest.Id, opt => opt.Ignore()); ;

        }
    }
}
