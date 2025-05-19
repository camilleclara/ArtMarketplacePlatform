using AutoMapper;
using BL.Models;
using Domain;

namespace BL.Mapper
{

    public class ByteArrayToBase64Resolver : IValueResolver<ProductImage, ProductImageDTO, string>
    {
        public string Resolve(ProductImage source, ProductImageDTO destination, string destMember, ResolutionContext context)
        {
            return source.Content != null ? Convert.ToBase64String(source.Content) : null;
        }
    }
}
