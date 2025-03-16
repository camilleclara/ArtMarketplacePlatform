using backend_app.Models;
using backend_app.Models.Enums;
using backend_app.Repositories.Interfaces;

namespace backend_app.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {

        Task<IEnumerable<Order>> GetByCustomerId(int id);

        Task<IEnumerable<Order>> GetByArtisanId(int id);

        Task<IEnumerable<Order>> GetByStatus(int attributeId);

        Task<Order> UpdateStatusById(int id, DeliveryStatus newStatus);
    }
}
