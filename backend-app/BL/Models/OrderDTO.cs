﻿using Domain;

namespace BL.Models
{
    public class OrderDTO
    {

        public OrderDTO() { }

        public int Id { get; set; }
        public int? ArtisanId { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ArtisanName { get; set; }
        public float? Total { get; set; }
        public DeliveryDTO ActiveDelivery { get; set; }
        public List<OrderedProductDTO>? Products { get; set; } = new();

    }
}
