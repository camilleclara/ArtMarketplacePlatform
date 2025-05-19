using System;
using System.Collections.Generic;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class MarketPlaceContext : DbContext
{
    public MarketPlaceContext()
    {
    }

    public MarketPlaceContext(DbContextOptions<MarketPlaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<DeliveryArtisanPartnership> DeliveryArtisanPartnerships { get; set; }

    public virtual DbSet<ItemOrder> ItemOrders { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Deliveri__3213E83FAB447223");

            entity.ToTable(tb => tb.HasTrigger("trg_Update_Deliveries"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.DeliStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("deli_status");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("delivery_date");
            entity.Property(e => e.EstimatedDate).HasColumnName("estimated_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Deliveries_Orders");

            entity.HasOne(d => d.Partner).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("FK__Deliverie__partn__6E01572D");
        });

        modelBuilder.Entity<DeliveryArtisanPartnership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F3E048A52");

            entity.ToTable(tb => tb.HasTrigger("trg_Update_DeliveryArtisanPartnerships"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArtisanId).HasColumnName("artisan_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.DeliveryPartnerId).HasColumnName("delivery_partner_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");

            entity.HasOne(d => d.Artisan).WithMany(p => p.DeliveryArtisanPartnershipArtisans)
                .HasForeignKey(d => d.ArtisanId)
                .HasConstraintName("FK_DeliveryArtisanPartnerships_Artisans");

            entity.HasOne(d => d.DeliveryPartner).WithMany(p => p.DeliveryArtisanPartnershipDeliveryPartners)
                .HasForeignKey(d => d.DeliveryPartnerId)
                .HasConstraintName("FK_DeliveryArtisanPartnerships_Partner");
        });

        modelBuilder.Entity<ItemOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item_Ord__3213E83FC12496B8");

            entity.ToTable("Item_Orders", tb => tb.HasTrigger("trg_Update_Item_Orders"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.ItemOrders)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_ItemOrders_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.ItemOrders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ItemOrders_Products");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Messages__3213E83FFA6CD293");

            entity.ToTable(tb => tb.HasTrigger("trg_Update_Messages"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.MsgFromId).HasColumnName("msg_from_id");
            entity.Property(e => e.MsgToId).HasColumnName("msg_to_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.MsgFrom).WithMany(p => p.MessageMsgFroms)
                .HasForeignKey(d => d.MsgFromId)
                .HasConstraintName("FK_Message_From");

            entity.HasOne(d => d.MsgTo).WithMany(p => p.MessageMsgTos)
                .HasForeignKey(d => d.MsgToId)
                .HasConstraintName("FK_Message_To");

            entity.HasOne(d => d.Product).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Message_Product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83FF2C651DD");

            entity.ToTable(tb => tb.HasTrigger("trg_Update_Orders"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArtisanId).HasColumnName("artisan_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");

            entity.HasOne(d => d.Artisan).WithMany(p => p.OrderArtisans)
                .HasForeignKey(d => d.ArtisanId)
                .HasConstraintName("FK_Orders_Artisans");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3213E83F30898AB0");

            entity.ToTable(tb => tb.HasTrigger("trg_Update_Products"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArtisanId).HasColumnName("artisan_id");
            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Artisan).WithMany(p => p.Products)
                .HasForeignKey(d => d.ArtisanId)
                .HasConstraintName("FK_Products_Artisans");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3213E83FA06CF177");

            entity.ToTable(tb => tb.HasTrigger("trg_Update_ProductImages"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.MimeType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mime_type");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductImages_Products");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3213E83FB1F51E75");

            entity.ToTable(tb => tb.HasTrigger("trg_Update_Reviews"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.FromArtisan).HasColumnName("fromArtisan");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Score).HasColumnName("score");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Reviews_Customers");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Reviews_Products");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FC8CF1179");

            entity.ToTable(tb => tb.HasTrigger("trg_Update_Users"));

            entity.HasIndex(e => e.Login, "UQ_Users_logins").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("full_address");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hashed_password");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("salt");
            entity.Property(e => e.UserType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
