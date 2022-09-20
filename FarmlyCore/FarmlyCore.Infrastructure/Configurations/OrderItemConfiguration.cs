using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmlyCore.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            entity.ToTable("OrderItems", "dbo");

            entity.HasKey(e => e.Id);

            entity.Property(k => k.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.ProductName)
                .HasColumnName("ProductName");

            entity.Property(e => e.Quantity)
                .HasColumnName("Quantity");

            entity.Property(e => e.Price)
                .HasColumnName("Price");

            entity.Property(e => e.AdvertItemPrice)
                .HasColumnName("AdvertItemPrice");

            entity.Property(e => e.PriceType)
                .HasColumnName("PriceType");

            entity.Property(e => e.Weight)
                .HasColumnName("Weight");

            entity.Property(e => e.PriceType)
                .HasColumnName("PriceType");

            entity.Property(e => e.ResponseStatus)
              .HasColumnName("ResponseStatus");

            entity.Property(e => e.FkAdvertItemId)
                .HasColumnName("FKAdvertItemID")
                .IsRequired();

            entity.Property(e => e.FkOrderId)
                .HasColumnName("FKOrderID")
                .IsRequired();

            entity.Property(e => e.FkCategoryId)
               .HasColumnName("FKCategoryID")
               .IsRequired();

            entity.Property(e => e.FkPickupPointId)
                .HasColumnName("FKPickupPointID")
                .IsRequired();

            entity.Property(e => e.FkSellerId)
                .HasColumnName("FKSellerID")
                .IsRequired();

            entity.Property(e => e.PlacementDate)
                .HasColumnName("PlacementDate")
                .IsRequired();

            entity.Property(e => e.PickupDate)
                .HasColumnName("PickupDate")
                .IsRequired();

            entity.Property(e => e.ResponseDate)
                .HasColumnName("ResponseDate");

            entity.HasOne(e => e.AdvertItem)
                 .WithOne()
                 .HasForeignKey<OrderItem>(e => e.FkAdvertItemId)
                 .IsRequired();

            entity.HasOne(e => e.Category)
                 .WithOne()
                 .HasForeignKey<OrderItem>(e => e.FkPickupPointId)
                 .IsRequired();

            entity.HasOne(e => e.Seller)
                .WithOne()
                .HasForeignKey<OrderItem>(e => e.FkSellerId)
                .IsRequired();

            entity.HasOne(e => e.Order)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.FkOrderId)
                .IsRequired();

            entity.HasOne(e => e.PickupPoint)
                 .WithOne()
                 .HasForeignKey<OrderItem>(e => e.FkPickupPointId)
                 .IsRequired();
        }
    }
}