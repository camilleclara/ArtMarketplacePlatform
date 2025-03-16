using System;
using System.Collections.Generic;

namespace backend_app.Models;

public partial class Delivery
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public bool IsActive { get; set; }

    public string? DeliStatus { get; set; }

    public DateOnly? EstimatedDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual Order? Order { get; set; }
}
