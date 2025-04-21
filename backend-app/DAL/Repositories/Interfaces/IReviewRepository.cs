using Domain;

namespace DAL.Repositories.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByProductId(int productId);

        Task<Review> SoftDeleteById(int id);

        Task SaveChanges();
        Task<IEnumerable<Review>> GetByArtisanId(int id);
    }
}
