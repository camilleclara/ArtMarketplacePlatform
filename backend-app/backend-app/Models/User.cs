using System;
using System.Collections.Generic;

namespace backend_app.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? HashedPassword { get; set; }

    public string? Salt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public bool? IsActive { get; set; }

    public string? UserType { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? Created { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<DeliveryArtisanPartnership> DeliveryArtisanPartnershipArtisans { get; set; } = new List<DeliveryArtisanPartnership>();

    public virtual ICollection<DeliveryArtisanPartnership> DeliveryArtisanPartnershipDeliveryPartners { get; set; } = new List<DeliveryArtisanPartnership>();

    public virtual ICollection<Order> OrderArtisans { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderCustomers { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
