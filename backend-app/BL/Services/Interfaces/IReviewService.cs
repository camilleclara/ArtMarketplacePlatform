using BL.Models;
using BL.Models.Enums;

namespace BL.Services.Interfaces

{
    public interface IReviewService: IGenericService<ReviewDTO>
    {
        
        Task<IEnumerable<ReviewDTO>> GetAllAsync();
        Task<ReviewDTO> GetByIdAsync(int id);
        Task<ReviewDTO> AddAsync(ReviewDTO entity);
        Task<ReviewDTO> UpdateAsync(int id, ReviewDTO entity);
        Task<int> DeleteAsync(int id);
        Task<ReviewDTO> AddAsyncForProduct(ReviewDTO review, int productId);
        Task<IEnumerable<ReviewDTO>> GetByArtisanId(int artisanId);
        Task<IEnumerable<ReviewDTO>> GetByProductId(int artisanId);

    }
}
