using System;
using System.Collections.Generic;

namespace backend_app.Models;

public partial class Message
{
    public int Id { get; set; }

    public int? ChatId { get; set; }

    public string? Content { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual Chat? Chat { get; set; }
}
