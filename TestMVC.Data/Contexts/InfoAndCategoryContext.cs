using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TestMVC.Data.Models;

namespace TestMVC.Data.Contexts
{
    public interface IInfoAndCategoryContext
    {
       
        Database GetDb();
        DbContext GetDbContext();
    }
    public class InfoAndCategoryContext : DbContext, IInfoAndCategoryContext
    {
        public InfoAndCategoryContext() : base("name=InfoAndCategoryContext") { }
       
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
            Database.SetInitializer<InfoAndCategoryContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
