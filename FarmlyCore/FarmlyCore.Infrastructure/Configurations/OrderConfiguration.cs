using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmlyCore.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("Orders", "dbo");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.OrderNumber)
                .HasColumnName("OrderNumber");

            entity.Property(e => e.TotalPrice)
                .HasColumnName("TotalPrice");

            entity.Property(e => e.PlacementDate)
                .HasColumnName("PlacementDate");

            entity.Property(e => e.DeliveryDate)
                .HasColumnName("DeliveryDate");

            entity.Property(e => e.Delivered)
                .HasColumnName("Delivered");

            entity.Property(e => e.BuyerId)
                .HasColumnName("FkBuyerCustomerId");

            entity.Property(e => e.DeliveryPointId)
                .HasColumnName("FkDeliveryPointId");

            entity.HasOne(e => e.Buyer)
                .WithOne()
                .HasForeignKey<Customer>(e => e.Id)
                .IsRequired();

            entity.HasOne(e => e.DeliveryPoint)
                .WithOne()
                .HasForeignKey<CustomerAddress>(e => e.Id)
                .IsRequired();
        }
    }
}