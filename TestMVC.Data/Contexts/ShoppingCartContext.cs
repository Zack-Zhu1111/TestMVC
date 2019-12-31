namespace TestMVC.Data.Contexts
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TestMVC.Data.Models;

    public interface IShoppingCartContext
    {
        DbSet<ShoppingCart> shoppingCart { get; set; }
        Database GetDb();
        DbContext GetDbContext();
    }
    public class ShoppingCartContext : DbContext,IShoppingCartContext
    {
        
        public ShoppingCartContext()
            : base("name=ShoppingCartContext")
        {
        }
        public virtual DbSet<ShoppingCart> shoppingCart { get; set; }
        public Database GetDb()
        {
            return Database;
        }
        public DbContext GetDbContext()
        {
            return this;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ShoppingCartContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }

}