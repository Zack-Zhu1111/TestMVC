using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TestMVC.Data.Models;


namespace TestMVC.Data.Contexts
{
    public interface IUserLoginContext
        {
            DbSet<UserLogin> UserLogin { get; set; }
            Database GetDb();
            DbContext GetDbContext();
        }
    public class UserLoginContext : DbContext,IUserLoginContext
    {
        public UserLoginContext() : base("name=UserLoginContext") { }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
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
            Database.SetInitializer<UserLoginContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
