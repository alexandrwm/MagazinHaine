﻿using BeStreet.Domain.Entities.Items;
using System.Data.Entity;

namespace BeStreet.BusinessLogic.DbContexts
{
    public class BeStreetContext : DbContext
    {
        public BeStreetContext() : base("name=BeStreet") { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Target> Targets { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}
