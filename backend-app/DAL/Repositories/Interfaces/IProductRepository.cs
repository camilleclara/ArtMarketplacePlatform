using Domain;

namespace DAL.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(string categoryString);
        Task<IEnumerable<Product>> GetAllAdmin();
        Task<IEnumerable<Product>> GetByAttributeId(int attributeId);

        Task<Product> SoftDeleteById(int id);

        Task SaveChanges();
        Task<List<int>> GetReviewableProductIdsByCustomerId(int customerId);
        Task<Product> ApproveById(int id);
    }
}
