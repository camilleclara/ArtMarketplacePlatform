﻿using System;
using System.Collections.Generic;

namespace Domain;

public partial class ItemOrder
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
