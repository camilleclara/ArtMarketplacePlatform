using System;
using System.Collections.Generic;

namespace Domain;

public partial class DeliveryArtisanPartnership
{
    public int Id { get; set; }

    public int? DeliveryPartnerId { get; set; }

    public int? ArtisanId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual User? Artisan { get; set; }

    public virtual User? DeliveryPartner { get; set; }
}
