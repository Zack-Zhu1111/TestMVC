using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVC.Data.Contexts;
using System.Data.Entity;
using System.Data.SqlClient;
using TestMVC.Domain.ViewModels;

namespace TestMVC.Provider
{
    public interface IShoppingCartProvider
    {
        List<ShoppingCartViewModel> GetById(string id);
        List<ShoppingCartViewModel> GetAll();
        List<ShoppingCartViewModel> GetByUserId(string userid);
        void InsertShopping(string userid,string id,string name,string price,int count);
        void DeleteShopping(string id);
        void DeleteUser(string userid);
    }
    public class ShoppingCartProvider : IShoppingCartProvider
    {
        private readonly IShoppingCartContext _db;
        private readonly DbContext _dbContext;
        public ShoppingCartProvider(IShoppingCartContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public void InsertShopping(string userid, string id, string name, string price, int count)
        {
            var storedProcedureName = "dbo.up_Common_ShoppingCart_Insert";
            try
            {
                SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                SqlParameter p2 = new SqlParameter("@Name", System.Data.SqlDbType.VarChar);
                p2.Value = name;
                SqlParameter p3 = new SqlParameter("@Price", System.Data.SqlDbType.VarChar);
                p3.Value = price;
                SqlParameter p4 = new SqlParameter("@Count", System.Data.SqlDbType.Int);
                p4.Value = count;
                SqlParameter p5 = new SqlParameter("@UserId", System.Data.SqlDbType.VarChar);
                p5.Value = userid;
                _dbContext.Database.ExecuteSqlCommand($"{storedProcedureName} @UserId,@ID,@Name,@Price,@Count", p5, p1, p2, p3, p4);
            }
            catch
            {
                throw;
            }
        }
        public List<ShoppingCartViewModel> GetById(string id)
        {
            var name = "dbo.up_Common_ShoppingCart_SelectById";
            SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
            p1.Value = id;
            var Vm = _dbContext.Database.SqlQuery<ShoppingCartViewModel>($"{name} @ID",p1).ToList();
            if(Vm.Count == 0)
            {
                Vm = null;
            }
            return Vm;
        }
        public List<ShoppingCartViewModel> GetAll()
        {
            var name = "dbo.up_Common_ShoppingCart_Select";
            var Vm = _dbContext.Database.SqlQuery<ShoppingCartViewModel>($"{name}").ToList();
            if (Vm.Count == 0)
            {
                Vm = null;
            }
            return Vm;
        }
        public List<ShoppingCartViewModel> GetByUserId(string userid)
        {
            var name = "dbo.up_Common_ShoppingCart_SelectByUserId";
            SqlParameter p1 = new SqlParameter("@UserId", System.Data.SqlDbType.VarChar);
            p1.Value = userid;
            var Vm = _dbContext.Database.SqlQuery<ShoppingCartViewModel>($"{name} @UserId",p1).ToList();
            if (Vm.Count == 0)
            {
                Vm = null;
            }
            return Vm;
        }
        public void DeleteShopping(string id)
        {
            var name = "dbo.up_Common_ShoppingCart_DeleteRow";
            SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
            p1.Value = id;
            _dbContext.Database.ExecuteSqlCommand($"{name} @ID", p1);
        }
        public void DeleteUser(string userid)
        {
            var name = "dbo.up_Common_ShoppingCart_DeleteByUserId";
            SqlParameter p1 = new SqlParameter("@UserId", System.Data.SqlDbType.VarChar);
            p1.Value = userid;
            _dbContext.Database.ExecuteSqlCommand($"{name} @UserId", p1);
        }
    }
}
