using backend_app.Models;
using backend_app.Models.Enums;
using backend_app.Repositories.Interfaces;

namespace backend_app.Repositories
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {

        Task<Delivery> UpdateStatusById(int id, string newStatus);

        Task<IEnumerable<Delivery>> GetByOrderId(int id);

    }
}
