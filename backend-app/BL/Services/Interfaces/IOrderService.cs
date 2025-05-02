using BL.Models;

namespace BL.Services.Interfaces

{
    public interface IOrderService : IGenericService<OrderDTO>
    {
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<IEnumerable<OrderDTO>> GetByCustomerIdAsync(int id);
        Task<IEnumerable<OrderDTO>> GetByArtisanIdAsync(int id);
        Task<OrderDTO> GetByIdAsync(int id);
        Task<OrderDTO> AddAsync(OrderDTO entity);
        Task<OrderDTO> UpdateAsync(int id, OrderDTO entity);
        Task<int> DeleteAsync(int id);
    }
}
