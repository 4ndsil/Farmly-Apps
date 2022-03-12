﻿using FarmlyCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Infrastructure.FarmlyDbContext
{
    public  class FarmlyEntityDbContext : DbContext
    {
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

        public virtual DbSet<Advert> Adverts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<User> Users { get; set; }        
    }
}
