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

            entity.Property(e => e.ProductName)
                .HasColumnName("ProductName");

            entity.Property(e => e.Quantity)
                .HasColumnName("Quantity");

            entity.Property(e => e.Price)
                .HasColumnName("Price");

            entity.Property(e => e.PriceType)
                .HasColumnName("PriceType");

            entity.Property(e => e.AdvertId)
                .HasColumnName("FkAdvertId")
                .IsRequired();

            entity.Property(e => e.OrderId)
                .HasColumnName("FkOrderId")
                .IsRequired();
        }
    }
}