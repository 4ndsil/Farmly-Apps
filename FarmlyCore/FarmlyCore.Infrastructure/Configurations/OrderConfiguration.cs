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

            entity.Property(k => k.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.OrderNumber)
                .HasColumnName("OrderNumber");

            entity.Property(e => e.TotalPrice)
                .HasColumnName("TotalPrice");

            entity.Property(e => e.TotalWeight)
                .HasColumnName("TotalWeight");

            entity.Property(e => e.PlacementDate)
                .HasColumnName("PlacementDate");

            entity.Property(e => e.DeliveryDate)
                .HasColumnName("DeliveryDate");

            entity.Property(e => e.EstimatedDeliveryDate)
                .HasColumnName("EstimatedDeliveryDate");

            entity.Property(e => e.FkBuyerId)
                .HasColumnName("FKBuyerID");

            entity.Property(e => e.FkDeliveryPointId)
                .HasColumnName("FKDeliveryPointID");

            entity.HasOne(e => e.Buyer)
                .WithOne()
                .HasForeignKey<Order>(e => e.FkBuyerId)
                .IsRequired();

            entity.HasOne(e => e.DeliveryPoint)
                .WithOne()
                .HasForeignKey<Order>(e => e.FkDeliveryPointId)
                .IsRequired();
        }
    }
}