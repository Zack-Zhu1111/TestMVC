using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TestMVC.Data.Models;

namespace TestMVC.Data.Contexts
{
    public interface IInfoMedicineContext
    {
        DbSet<InfoMedicine> InfoMedicine { get; set; }
        Database GetDb();
        DbContext GetDbContext();
    }
    public class InfoMedicineContext : DbContext, IInfoMedicineContext
    {
        public InfoMedicineContext() : base("name=InfoMedicineContext") { }
        public virtual DbSet<InfoMedicine> InfoMedicine { get; set; }
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
            Database.SetInitializer<InfoMedicineContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
