using DAL.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly MarketPlaceContext _context;

        public StatisticsRepository(MarketPlaceContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalOrdersCountAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<double> GetTotalRevenueAsync()
        {
            // Calculate total revenue by summing the product price * quantity for all order items
            return (double)await _context.ItemOrders
                .Include(i => i.Product)
                .Where(i => i.ProductId != null && i.Product.Price != null)
                .SumAsync(i => i.Product.Price.Value * i.Quantity);
        }

        public async Task<int> GetPendingOrdersCountAsync()
        {
            // Count orders that have active deliveries with status "PROCESSING" or "SHIPPED"
            return await _context.Orders
                .Where(o => o.Deliveries.Any(d => d.IsActive && (d.DeliStatus == "PROCESSING" || d.DeliStatus == "SHIPPED")))
                .CountAsync();
        }

        public async Task<int> GetCompletedOrdersCountAsync()
        {
            // Count orders that have active deliveries with status "DELIVERED"
            return await _context.Orders
                .Where(o => o.Deliveries.Any(d => d.IsActive && d.DeliStatus == "DELIVERED"))
                .CountAsync();
        }

        public async Task<Dictionary<string, int>> GetOrdersByStatusAsync()
        {
            // Group orders by delivery status and count
            var query = await _context.Deliveries
                .Where(d => d.IsActive)
                .GroupBy(d => d.DeliStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return query.ToDictionary(x => x.Status ?? "UNDEFINED", x => x.Count);
        }

        public async Task<Dictionary<string, double>> GetRevenueByMonthAsync()
        {
            // Group orders by month and calculate revenue
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);

            var query = await _context.Orders
                .Where(o => o.Created >= sixMonthsAgo)
                .Include(o => o.ItemOrders)
                .ThenInclude(i => i.Product)
                .ToListAsync();

            var result = query
                .GroupBy(o => new { Month = o.Created.Value.Month, Year = o.Created.Value.Year })
                .Select(g => new
                {
                    MonthYear = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Revenue = (double) g.SelectMany(o => o.ItemOrders)
                    .Where(i => i.Product != null && i.Product.Price != null)
                    .Sum(i => i.Product!.Price!.Value * i.Quantity)
                })
                .OrderBy(x => x.MonthYear)
                .ToDictionary(x => x.MonthYear, x => x.Revenue);

            return result;
        }

        public async Task<(IEnumerable<Product> Products, Dictionary<int, int> SalesCounts)> GetTrendingProductsAsync(int limit = 10)
        {
            // Récupérer les produits populaires
            var productIds = await _context.ItemOrders
                .Where(i => i.ProductId != null)
                .GroupBy(i => i.ProductId.Value)
                .Select(g => new { ProductId = g.Key, OrderCount = g.Count() })
                .OrderByDescending(x => x.OrderCount)
                .Take(limit)
                .Select(x => x.ProductId)
                .ToListAsync();

            // Récupérer les produits avec navigation
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .Include(p => p.Artisan)
                .Include(p => p.Reviews)
                .Include(p => p.ItemOrders)
                .Include(p => p.ProductImages)
                .ToListAsync();

            // Récupérer les ventes totales par produit
            var salesCounts = await _context.ItemOrders
                 .Where(i => productIds.Contains(i.ProductId.Value))
                 .GroupBy(i => i.ProductId.Value)
                 .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) ?? 0 }) // Si la somme est null, utiliser 0
                 .ToDictionaryAsync(x => x.ProductId, x => x.Quantity);


            // Passe les produits et le dictionnaire en retour (si tu veux juste le mapper après dans le contrôleur)
            return (products, salesCounts);

        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetActiveUsersCountAsync()
        {
            return await _context.Users.Where(u => u.IsActive).CountAsync();
        }

        public async Task<int> GetUserCountByTypeAsync(string userType)
        {
            return await _context.Users.CountAsync(u => u.UserType == userType);
        }

        public async Task<Dictionary<string, int>> GetNewUsersByMonthAsync()
        {
            // Group users by creation month over the last 6 months
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);

            var query = await _context.Users
                .Where(u => u.Created >= sixMonthsAgo)
                .ToListAsync();

            var result = query
                .GroupBy(u => new { Month = u.Created.Value.Month, Year = u.Created.Value.Year })
                .Select(g => new
                {
                    MonthYear = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Count = g.Count()
                })
                .OrderBy(x => x.MonthYear)
                .ToDictionary(x => x.MonthYear, x => x.Count);

            return result;
        }

        public async Task<Dictionary<string, int>> GetUsersByTypeAsync()
        {
            // Group users by type and count
            var query = await _context.Users
                .GroupBy(u => u.UserType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();

            return query.ToDictionary(x => x.Type ?? "UNDEFINED", x => x.Count);
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int limit = 10)
        {
            // Get most recent orders with their associated data
            return await _context.Orders
                .OrderByDescending(o => o.Created)
                .Take(limit)
                .Include(o => o.Artisan)
                .Include(o => o.Customer)
                .Include(o => o.ItemOrders)
                    .ThenInclude(i => i.Product)
                .Include(o => o.Deliveries.Where(d => d.IsActive))
                .ToListAsync();
        }
    }
}