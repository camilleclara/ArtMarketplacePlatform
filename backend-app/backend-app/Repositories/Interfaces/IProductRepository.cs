using backend_app.Models;

namespace backend_app.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(string categoryString);

        Task<IEnumerable<Product>> GetByAttributeId(int attributeId);

        Task<Product> SoftDeleteById(int id);
    }
}
