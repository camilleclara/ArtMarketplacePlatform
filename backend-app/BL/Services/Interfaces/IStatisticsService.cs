using BL.Models;
using BL.Models.Statistics;

namespace BL.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<OrderStatisticsDTO> GetOrderStatisticsAsync();
        Task<IEnumerable<TrendingProductDTO>> GetTrendingProductsAsync(int limit = 10);
        Task<UserActivityStatisticsDTO> GetUserActivityStatisticsAsync();
        Task<IEnumerable<OrderDTO>> GetRecentOrdersAsync(int limit = 10);
    }
}