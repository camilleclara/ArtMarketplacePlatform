using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
//using backend_app.Repositories;

namespace backend_app.Services.Interfaces
{
    public interface IDeliveryService: IGenericService<DeliveryDTO>
    {
        Task<IEnumerable<DeliveryDTO>> GetAllAsync();
        Task<DeliveryDTO> GetByIdAsync(int id);
        Task<DeliveryDTO> AddAsync(DeliveryDTO entity);
        Task<DeliveryDTO> UpdateAsync(int id, DeliveryDTO entity);
        Task<int> DeleteAsync(int id);
        Task<DeliveryDTO> GetActiveDeliveryForOrder(int orderId);
        Task<int> GetActiveDeliveryIdForOrder(int orderId);


    }
}
