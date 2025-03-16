using System;
using System.Collections.Generic;

namespace backend_app.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? ArtisanId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double? Price { get; set; }

    public string? Category { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual User? Artisan { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
