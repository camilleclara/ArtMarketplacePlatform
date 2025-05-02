using BL.Models;
using BL.Models.Enums;

namespace BL.Services.Interfaces

{
    public interface IProductService: IGenericService<ProductDTO>
    {
        //public Task<IEnumerable<ProductDTO>> GetAllAsync();
        //public Task<ProductDTO> GetById(int id);
        //public Task<ProductDTO> AddAsync(ProductDTO product);
        //public Task<ProductDTO> AddAsyncForArtisan(ProductDTO product, int artisanId);
        //public Task<ProductDTO> UpdateAsync(int id, ProductDTO product);
        //public Task<IEnumerable<ProductDTO>> GetByCategory(Category category);
        //public Task<IEnumerable<ProductDTO>> GetByArtisanId(int artisanId);
        //public Task<bool> SoftDeleteProductAsync(int productId);
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> AddAsync(ProductDTO entity);
        Task<ProductDTO> UpdateAsync(int id, ProductDTO entity);
        Task<int> DeleteAsync(int id);

        Task<IEnumerable<ProductDTO>> GetByCategoryAsync(Category category);
        Task<ProductDTO> AddAsyncForArtisan(ProductDTO product, int artisanId);
        Task<IEnumerable<ProductDTO>> GetByArtisanId(int artisanId);

        Task<List<int>> GetReviewableProductIdsByCustomerId(int customerId);


    }
}
