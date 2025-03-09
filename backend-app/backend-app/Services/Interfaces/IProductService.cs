using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;

namespace backend_app.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAllAsync();
        public Task<ProductDTO> GetById(int id);
        public Task<ProductDTO> AddAsync(ProductDTO product);
        public Task<ProductDTO> UpdateAsync(int id, ProductDTO product);
        public Task<IEnumerable<ProductDTO>> GetByCategory(Category category);
        public Task<IEnumerable<ProductDTO>> GetByArtisanId(int artisanId);

    }
}
