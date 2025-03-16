using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using backend_app.Repositories;

namespace backend_app.Services.Interfaces
{
    public interface IOrderService : IGenericService<OrderDTO>
    {
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<IEnumerable<OrderDTO>> GetByCustomerId(int id);
        Task<OrderDTO> GetByIdAsync(int id);
        Task<OrderDTO> AddAsync(OrderDTO entity);
        Task<OrderDTO> UpdateAsync(int id, OrderDTO entity);
        Task<int> DeleteAsync(int id);
    }
}
