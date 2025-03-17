using BL.Models;

namespace BL.Services.Interfaces

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
