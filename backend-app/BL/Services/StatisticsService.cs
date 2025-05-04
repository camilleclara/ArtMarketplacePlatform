using AutoMapper;
using BL.Models;
using BL.Models.Statistics;
using BL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IMapper _mapper;

        public StatisticsService(IStatisticsRepository statisticsRepository, IMapper mapper)
        {
            _statisticsRepository = statisticsRepository;
            _mapper = mapper;
        }

        public async Task<OrderStatisticsDTO> GetOrderStatisticsAsync()
        {
            var totalOrders = await _statisticsRepository.GetTotalOrdersCountAsync();
            var totalRevenue = await _statisticsRepository.GetTotalRevenueAsync();
            var pendingOrders = await _statisticsRepository.GetPendingOrdersCountAsync();
            var completedOrders = await _statisticsRepository.GetCompletedOrdersCountAsync();
            var ordersByStatus = await _statisticsRepository.GetOrdersByStatusAsync();
            var revenueByMonth = await _statisticsRepository.GetRevenueByMonthAsync();

            var orderStatistics = new OrderStatisticsDTO
            {
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                PendingOrders = pendingOrders,
                CompletedOrders = completedOrders,
                OrdersByStatus = ordersByStatus,
                RevenueByMonth = revenueByMonth,
                AverageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0
            };

            return orderStatistics;
        }

        public async Task<IEnumerable<TrendingProductDTO>> GetTrendingProductsAsync(int limit = 10)
        {
            var (products, salesCounts) = await _statisticsRepository.GetTrendingProductsAsync();
            var trendingProducts = products.Select(p => _mapper.Map<TrendingProductDTO>(p, opt =>
            {
                opt.Items["SalesCounts"] = salesCounts;
            })).ToList();
            return trendingProducts;

        }

        public async Task<UserActivityStatisticsDTO> GetUserActivityStatisticsAsync()
        {
            var totalUsers = await _statisticsRepository.GetTotalUsersCountAsync();
            var activeUsers = await _statisticsRepository.GetActiveUsersCountAsync();
            var totalArtisans = await _statisticsRepository.GetUserCountByTypeAsync("ARTISAN");
            var totalCustomers = await _statisticsRepository.GetUserCountByTypeAsync("CUSTOMER");
            var totalDeliveryPartners = await _statisticsRepository.GetUserCountByTypeAsync("DELIVERYPARTNER");
            var newUsersByMonth = await _statisticsRepository.GetNewUsersByMonthAsync();
            var usersByType = await _statisticsRepository.GetUsersByTypeAsync();

            var userActivityStatistics = new UserActivityStatisticsDTO
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                TotalArtisans = totalArtisans,
                TotalCustomers = totalCustomers,
                TotalDeliveryPartners = totalDeliveryPartners,
                NewUsersByMonth = newUsersByMonth,
                UsersByType = usersByType
            };

            return userActivityStatistics;
        }

        public async Task<IEnumerable<OrderDTO>> GetRecentOrdersAsync(int limit = 10)
        {
            var recentOrders = await _statisticsRepository.GetRecentOrdersAsync(limit);
            return _mapper.Map<IEnumerable<OrderDTO>>(recentOrders);
        }
    }
}