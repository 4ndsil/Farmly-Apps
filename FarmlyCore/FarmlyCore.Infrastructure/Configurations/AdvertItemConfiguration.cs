using FarmlyCore.Infrastructure.Entites;
using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmlyCore.Infrastructure.Configurations
{
    public class AdvertItemConfiguration : IEntityTypeConfiguration<AdvertItem>
    {
        public void Configure(EntityTypeBuilder<AdvertItem> entity)
        {
            entity.ToTable("AdvertItems", "dbo");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Price)
                .HasColumnName("Price");

            entity.Property(e => e.Quantity)
                .HasColumnName("Quantity");

            entity.Property(e => e.FkAdvertId)
                .HasColumnName("FKAdvertId")
                .IsRequired();

            entity.HasOne(e => e.Advert)
              .WithOne()
              .HasForeignKey<Advert>(e => e.Id)
              .IsRequired();
        }
    }
}
