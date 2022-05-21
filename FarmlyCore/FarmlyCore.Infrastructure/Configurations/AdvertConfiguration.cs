using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmlyCore.Infrastructure.Configurations
{
    public class AdvertConfiguration : IEntityTypeConfiguration<Advert>
    {
        public void Configure(EntityTypeBuilder<Advert> entity)
        {
            entity.ToTable("Adverts", "dbo");

            entity.HasKey(p => p.Id);

            entity.Property(k => k.Id).ValueGeneratedOnAdd();

            entity.Property(p => p.Description)
                .HasColumnName("Description");

            entity.Property(p => p.Available)
               .HasColumnName("Available")
               .IsRequired();

            entity.Property(p => p.PriceType)
                .HasColumnName("PriceType");

            entity.Property(p => p.TotalQuantity)
                .HasColumnName("TotalQuantity");

            entity.Property(p => p.Price)
               .HasColumnName("Price");

            entity.Property(p => p.ProductName)
                .HasColumnName("ProductName");

            entity.Property(p => p.InsertDate)
                .HasColumnName("InsertDate");

            entity.Property(p => p.FkCategoryId)
                .HasColumnName("FKCategoryID")
                .IsRequired();

            entity.Property(p => p.FkPickupPointId)
                .HasColumnName("FKPickupPointID")
                .IsRequired();

            entity.Property(p => p.FkSellerId)
                .HasColumnName("FKSellerID")
                .IsRequired();

            entity.HasOne(p => p.Category)
                .WithOne()
                .HasForeignKey<Advert>(p => p.FkCategoryId)
                .IsRequired();

            entity.HasOne(p => p.PickupPoint)
               .WithOne()
               .HasForeignKey<Advert>(p => p.FkPickupPointId);

            entity.HasOne(p => p.Seller)
                .WithOne()
                .HasForeignKey<Advert>(p => p.FkSellerId)
                .IsRequired();
        }
    }
}
