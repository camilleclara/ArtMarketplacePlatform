using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetTotalOrdersCountAsync();
        Task<double> GetTotalRevenueAsync();
        Task<int> GetPendingOrdersCountAsync();
        Task<int> GetCompletedOrdersCountAsync();
        Task<Dictionary<string, int>> GetOrdersByStatusAsync();
        Task<Dictionary<string, double>> GetRevenueByMonthAsync();
        Task<(IEnumerable<Product> Products, Dictionary<int, int> SalesCounts)> GetTrendingProductsAsync(int limit = 10);
        Task<int> GetTotalUsersCountAsync();
        Task<int> GetActiveUsersCountAsync();
        Task<int> GetUserCountByTypeAsync(string userType);
        Task<Dictionary<string, int>> GetNewUsersByMonthAsync();
        Task<Dictionary<string, int>> GetUsersByTypeAsync();
        Task<IEnumerable<Order>> GetRecentOrdersAsync(int limit = 10);
    }
}