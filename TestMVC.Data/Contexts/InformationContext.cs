namespace TestMVC.Data.Contexts
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TestMVC.Data.Models;

    public interface IInformationContext
    {
        DbSet<Information> Information { get; set; }
        Database GetDb();
        DbContext GetDbContext();
    }
    public class InformationContext : DbContext, IInformationContext
    {
        
        public InformationContext()
            : base("name=InformationContext") {}
        public virtual DbSet<Information> Information { get; set; }
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
            Database.SetInitializer<InformationContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }

    
}