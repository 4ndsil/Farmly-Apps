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

            entity.HasKey(e => e.Id);

            entity.Property(e => e.CategoryName)
                .HasColumnName("CategoryName");

            entity.Property(e => e.CategoryType)
                .HasColumnName("CategoryType");
        }
    }
}
