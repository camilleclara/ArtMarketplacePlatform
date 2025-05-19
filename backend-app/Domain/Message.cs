using System;
using System.Collections.Generic;

namespace Domain;

public partial class Message
{
    public int Id { get; set; }

    public int? MsgFromId { get; set; }

    public int? MsgToId { get; set; }

    public int? ProductId { get; set; }

    public string? Content { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual User? MsgFrom { get; set; }

    public virtual User? MsgTo { get; set; }

    public virtual Product? Product { get; set; }
}
