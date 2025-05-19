using System;
using System.Collections.Generic;

namespace BL.Models.Statistics
{
    public class OrderStatisticsDTO
    {
        public int TotalOrders { get; set; }
        public double TotalRevenue { get; set; }
        public int PendingOrders { get; set; }
        public int CompletedOrders { get; set; }
        public Dictionary<string, int> OrdersByStatus { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, double> RevenueByMonth { get; set; } = new Dictionary<string, double>();
        public double AverageOrderValue { get; set; }
    }

    public class TrendingProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int OrderCount { get; set; }
        public double TotalRevenue { get; set; }
        public string ArtisanName { get; set; }
        public string Category { get; set; }
        public double? AverageRating { get; set; }
        public int SalesCount { get; set; }
        public List<ProductImageDTO> ProductImages { get; set; }
    }

    public class UserActivityStatisticsDTO
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalArtisans { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalDeliveryPartners { get; set; }
        public Dictionary<string, int> NewUsersByMonth { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> UsersByType { get; set; } = new Dictionary<string, int>();
    }

}