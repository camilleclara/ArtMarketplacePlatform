using Domain;

namespace DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {

        Task<IEnumerable<Order>> GetByCustomerId(int id);

        Task<IEnumerable<Order>> GetByArtisanId(int id);

        Task<IEnumerable<Order>> GetByStatus(int attributeId);

        Task<Order> UpdateStatusById(int id, string newStatus);
    }
}
