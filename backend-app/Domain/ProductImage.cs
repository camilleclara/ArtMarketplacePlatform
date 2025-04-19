using System;
using System.Collections.Generic;

namespace Domain;

public partial class ProductImage
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public byte[]? Content { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public string? Name { get; set; }

    public string? MimeType { get; set; }

    public virtual Product? Product { get; set; }
}
