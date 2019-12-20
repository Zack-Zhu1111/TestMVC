using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TestMVC.Data.Models;

namespace TestMVC.Data.Contexts
{
    public interface IMedicineCategoryContext
    {
        DbSet<MedicineCategory> MedicineCategory { get; set; }
        Database GetDb();
        DbContext GetDbContext();
    }
    public class MedicineCategoryContext : DbContext, IMedicineCategoryContext
    {
        public MedicineCategoryContext() : base("name=MedicineCategoryContext") { }
        public virtual DbSet<MedicineCategory> MedicineCategory { get; set; }
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
            Database.SetInitializer<MedicineCategoryContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
