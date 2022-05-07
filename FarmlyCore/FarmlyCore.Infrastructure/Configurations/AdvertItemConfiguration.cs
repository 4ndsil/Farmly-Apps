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

            entity.HasKey(k => k.Id);

            entity.Property(k => k.Id).ValueGeneratedOnAdd();

            entity.Property(p => p.Price)
                .HasColumnName("Price");

            entity.Property(p => p.Quantity)
                .HasColumnName("Quantity");

            entity.Property(p => p.FkAdvertId)
                .HasColumnName("FKAdvertId")
                .IsRequired();

            entity.HasOne(p => p.Advert)
              .WithMany(p => p.AdvertItems)
              .HasForeignKey(p => p.FkAdvertId);              
        }
    }
}
