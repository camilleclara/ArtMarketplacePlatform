using System;
using System.Collections.Generic;

namespace backend_app.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? ArtisanId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual User? Artisan { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
}
