
using AutoMapper;
using BL.Models;
using Domain;

namespace BL.Mapper;

public class Base64ToByteArrayResolver : IValueResolver<ProductImageDTO, ProductImage, byte[]>
{
    public byte[] Resolve(ProductImageDTO source, ProductImage destination, byte[] destMember, ResolutionContext context)
    {
        return string.IsNullOrEmpty(source.Content) ? null : Convert.FromBase64String(source.Content);
    }
}

