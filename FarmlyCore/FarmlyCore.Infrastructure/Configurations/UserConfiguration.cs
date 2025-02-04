﻿using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmlyCore.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users", "dbo");

            entity.HasKey(e => e.Id);

            entity.Property(k => k.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.FirstName)
                .HasColumnName("FirstName");

            entity.Property(e => e.LastName)
                .HasColumnName("LastName");

            entity.Property(e => e.Email)
               .HasColumnName("Email");

            entity.Property(e => e.Password)
                .HasColumnName("Password");

            entity.Property(e => e.FkCustomerId)
                .HasColumnName("FKCustomerId")
                .IsRequired();

            entity.HasOne(e => e.Customer)
                .WithOne()
                .HasForeignKey<User>(e => e.FkCustomerId)
                .IsRequired();
        }
    }
}