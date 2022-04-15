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

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Description)
                .HasColumnName("Description")
                .IsRequired();

            entity.Property(e => e.PriceType)
                .HasColumnName("PriceType");

            entity.Property(e => e.TotalQuantity)
                .HasColumnName("TotalQuantity");

            entity.Property(e => e.ProductName)
                .HasColumnName("ProductName");

            entity.Property(e => e.FkCategoryId)
                .HasColumnName("FKCategoryId")
                .IsRequired();

            entity.Property(e => e.FkPickupPointId)
                .HasColumnName("FKCustomerAddressId")
                .IsRequired();

            entity.Property(e => e.FkSellerId)
                .HasColumnName("FKSellerCustomerId")
                .IsRequired();

            entity.HasOne(e => e.Category)
                .WithOne()
                .HasForeignKey<Category>(e => e.Id)
                .IsRequired();

            entity.HasOne(e => e.PickupPoint)
                .WithOne()
                .HasForeignKey<CustomerAddress>(e => e.Id)
                .IsRequired();

            entity.HasOne(e => e.Seller)
                .WithOne()
                .HasForeignKey<Customer>(e => e.Id)
                .IsRequired();          
        }
    }
}
