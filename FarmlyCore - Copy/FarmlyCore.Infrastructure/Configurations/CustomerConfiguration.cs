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

            entity.HasKey(k => k.Id);

            entity.Property(k => k.Id).ValueGeneratedOnAdd();            

            entity.Property(p => p.CompanyName)
                .HasColumnName("CompanyName");

            entity.Property(p => p.CustomerType)
                .HasColumnName("CustomerType");

            entity.Property(p => p.BankGiro)
                .HasColumnName("BankGiro");

            entity.Property(p => p.OrgNumber)
                .HasColumnName("OrgNumber");

            entity.HasMany(p => p.CustomerAddresses)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.FkCustomerId);                
        }
    }
}