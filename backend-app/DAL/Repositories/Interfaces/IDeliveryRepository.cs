
using Domain;

namespace DAL.Repositories.Interfaces
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {

        Task<Delivery> UpdateStatusById(int id, string newStatus);

        Task<IEnumerable<Delivery>> GetByOrderId(int id);

    }
}
