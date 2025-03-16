using System;
using System.Collections.Generic;

namespace backend_app.Models;

public partial class Chat
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? CustomerId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Product? Product { get; set; }
}
