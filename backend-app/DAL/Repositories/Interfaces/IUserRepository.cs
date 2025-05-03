using Domain;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> DeactivateAsync(int id);
        Task<User> ApproveAsync(int id);
    }
}
