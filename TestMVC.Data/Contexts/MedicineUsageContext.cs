using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TestMVC.Data.Models;

namespace TestMVC.Data.Contexts
{
    public interface IMedicineUsageContext
    {
        DbSet<MedicineUsage> MedicineUsage { get; set; }
        Database GetDb();
        DbContext GetDbContext();
    }
    public class MedicineUsageContext : DbContext, IMedicineUsageContext
    {
        public MedicineUsageContext() : base("name=MedicineUsageContext") { }
        public virtual DbSet<MedicineUsage> MedicineUsage { get; set; }
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
            Database.SetInitializer<MedicineUsageContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
