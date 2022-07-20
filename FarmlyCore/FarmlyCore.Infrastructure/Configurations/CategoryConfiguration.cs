using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmlyCore.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Categories", "dbo");

            entity.HasKey(k => k.Id);

            entity.Property(k => k.Id).ValueGeneratedOnAdd();

            entity.Property(p => p.CategoryName)
                .HasColumnName("CategoryName");
        }
    }
}