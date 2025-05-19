using System;
using System.Collections.Generic;

namespace Domain;

public partial class Review
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? CustomerId { get; set; }

    public string? Content { get; set; }

    public bool? FromArtisan { get; set; }

    public bool IsActive { get; set; }

    public int? Score { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual User? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
