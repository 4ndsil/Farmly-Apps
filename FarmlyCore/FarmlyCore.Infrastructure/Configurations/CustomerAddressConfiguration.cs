using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmlyCore.Infrastructure.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> entity)
        {
            entity.ToTable("CustomerAddresses", "dbo");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Street)
                .HasColumnName("Street");

            entity.Property(e => e.City)
                .HasColumnName("City");

            entity.Property(e => e.State)
                .HasColumnName("State");

            entity.Property(e => e.Zip)
                .HasColumnName("Zip");

            entity.Property(e => e.FkCustomerId)                
                .HasColumnName("FKCustomerID");

            entity.HasOne(e => e.Customer)
              .WithMany(e => e.CustomerAddresses)
              .HasForeignKey(e => e.FkCustomerId)
              .IsRequired();
        }
    }
}
