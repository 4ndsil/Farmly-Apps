using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmlyCore.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customers", "dbo");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
             .HasColumnName("Id")
                .IsRequired();

            entity.Property(e => e.CompanyName)
                .HasColumnName("CompanyName");

            entity.Property(e => e.CustomerType)
                .HasColumnName("CustomerType");

            entity.Property(e => e.BankGiro)
                .HasColumnName("BankGiro");

            entity.Property(e => e.OrgNumber)
                .HasColumnName("OrgNumber");

            entity.HasMany(e => e.CustomerAddresses)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.FkCustomerId);
        }
    }
}