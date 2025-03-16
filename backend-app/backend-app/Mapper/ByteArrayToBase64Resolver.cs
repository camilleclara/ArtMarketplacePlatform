namespace backend_app.Mapper
{
    using AutoMapper;
    using backend_app.Models.DTO;
    using backend_app.Models;

    public class ByteArrayToBase64Resolver : IValueResolver<ProductImage, ProductImageDTO, string>
    {
        public string Resolve(ProductImage source, ProductImageDTO destination, string destMember, ResolutionContext context)
        {
            return source.Content != null ? Convert.ToBase64String(source.Content) : null;
        }
    }
}
