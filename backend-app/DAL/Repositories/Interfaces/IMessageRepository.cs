using Domain;

namespace DAL.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetByProductId(int productId);
        Task SaveChanges();
        Task<IEnumerable<Message>> GetByUserId(int id);
    }
}
