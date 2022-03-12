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

            entity.Property(e => e.Price)
                .HasColumnName("Price");

            entity.Property(e => e.PriceType)
                .HasColumnName("PriceType");

            entity.Property(e => e.Quantity)
                .HasColumnName("Quantity");            

            entity.Property(e => e.ProductName)
                .HasColumnName("ProductName");

            entity.Property(e => e.CategoryId)
                .HasColumnName("FkCategoryId")
                .IsRequired();

            entity.Property(e => e.PickupPointId)
                .HasColumnName("FkCustomerAddressId")
                .IsRequired();

            entity.Property(e => e.SellerId)
                .HasColumnName("FkSellerCustomerId")
                .IsRequired();

            entity.HasOne(e => e.Seller)
                .WithOne()
                .HasForeignKey<Customer>(e => e.Id)
                .IsRequired();

            entity.HasOne(e => e.Category)
                .WithOne()
                .HasForeignKey<Category>(e => e.Id)
                .IsRequired();

            entity.HasOne(e => e.PickupPoint)
            .WithOne()
            .HasForeignKey<CustomerAddress>(e => e.Id)
            .IsRequired();
        }
    }
}
