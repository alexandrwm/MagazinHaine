using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.User;
using System.Data.Entity;

namespace BeStreet.BusinessLogic.DbContexts
{
    public class BeStreetContext : DbContext
    {
        public BeStreetContext() : base("name=BeStreet") { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Target> Targets { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDtl> CartDtls { get; set;}
    }
}
