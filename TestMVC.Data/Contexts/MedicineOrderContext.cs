namespace TestMVC.Data.Contexts
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TestMVC.Data.Models;


    public interface IMedicineOrderContext
    {
        DbSet<MedicineOrder> Order { get; set; }
        Database GetDb();
        DbContext GetDbContext();
    }
    public class MedicineOrderContext : DbContext, IMedicineOrderContext
    {
       
        public MedicineOrderContext()
            : base("name=MedicineOrderContext")
        {
        }
        public virtual DbSet<MedicineOrder> Order { get; set; }
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
            Database.SetInitializer<MedicineOrderContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
    
}