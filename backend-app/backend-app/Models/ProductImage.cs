using System;
using System.Collections.Generic;

namespace backend_app.Models;

public partial class ProductImage
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int? ProductId { get; set; }

    public byte[]? Content { get; set; }
    public string MimeType { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual Product? Product { get; set; }
}
