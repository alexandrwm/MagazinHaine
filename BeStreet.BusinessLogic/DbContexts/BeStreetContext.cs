using BeStreet.Domain.Entities.Items;
using System.Data.Entity;

namespace BeStreet.BusinessLogic.DbContexts
{
    public class BeStreetContext : DbContext
    {
        public BeStreetContext() : base("name=BeStreet") { }

        public virtual DbSet<Customer> Customers { get; set; }
        //public virtual DbSet<BuyDtl> BuyDtls { get; set; }

        //public virtual DbSet<Buying> Buyings { get; set; }

        //public virtual DbSet<Cart> Carts { get; set; }

        //public virtual DbSet<CartDtl> CartDtls { get; set; }

        //public virtual DbSet<Color> Colors { get; set; }

        //public virtual DbSet<Duty> Duties { get; set; }

        //public virtual DbSet<Product> Products { get; set; }

        //public virtual DbSet<ProductType> ProductTypes { get; set; }

        //public virtual DbSet<Size> Sizes { get; set; }

        //public virtual DbSet<Staff> Staffs { get; set; }

        //public virtual DbSet<Status> Statuses { get; set; }

        //public virtual DbSet<Supplier> Suppliers { get; set; }

        //public virtual DbSet<Target> Targets { get; set; }
    }
}
