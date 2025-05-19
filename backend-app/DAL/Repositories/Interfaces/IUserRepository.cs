using Domain;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> DeactivateAsync(int id);
        Task<User> ApproveAsync(int id);
        Task<User> Update(int id, User entity, bool adminRights);
        Task<IEnumerable<User>> GetAllDeliveryPartners();
        Task<IEnumerable<User>> GetAllArtisans();
    }
}
