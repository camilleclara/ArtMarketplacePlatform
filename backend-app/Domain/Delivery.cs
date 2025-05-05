using System;
using System.Collections.Generic;

namespace Domain;

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

    public int? PartnerId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? Partner { get; set; }
}
