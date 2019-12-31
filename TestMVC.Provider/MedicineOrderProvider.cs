using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVC.Domain.ViewModels;
using TestMVC.Data.Contexts;
using System.Data.Entity;
using System.Data.SqlClient;

namespace TestMVC.Provider
{
    public interface IMedicineOrderProvider
    {
        List<MedicineOrderViewModel> GetOrder(string userid);
        void DeleteOrder(string time, string userid);
        void DeleteAll(string userid);
        void InsertOrder(string time, string id, string name, int count, string userid);
    }
    public class MedicineOrderProvider : IMedicineOrderProvider
    {
        private readonly IMedicineOrderContext _db;
        private readonly DbContext _dbContext;
        public MedicineOrderProvider(IMedicineOrderContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public List<MedicineOrderViewModel> GetOrder(string userid)
        {
            var Stored = "dbo.up_Common_MedicineOrder_SelectOrder";
            SqlParameter p1 = new SqlParameter("@UserId", System.Data.SqlDbType.VarChar);
            p1.Value = userid;
            var list = _dbContext.Database.SqlQuery<MedicineOrderViewModel>($"{Stored} @UserId",p1).ToList();
            return list;
        }
        public void DeleteOrder(string time, string userid)
        {
            var Stored = "dbo.up_Common_MedicineOrder_DeleteOrder";
            SqlParameter p1 = new SqlParameter("@Time", System.Data.SqlDbType.VarChar);
            p1.Value = time;
            SqlParameter p2 = new SqlParameter("@UserId", System.Data.SqlDbType.VarChar);
            p2.Value = userid;
            _dbContext.Database.ExecuteSqlCommand($"{Stored} @Time,@UserId", p1,p2);
        }
        public void DeleteAll(string userid)
        {
            SqlParameter p1 = new SqlParameter("@UserId", System.Data.SqlDbType.VarChar);
            p1.Value = userid;
            var Stored = "dbo.up_Common_MedicineOrder_DeleteAllOrder";
            _dbContext.Database.ExecuteSqlCommand($"{Stored} @UserId",p1);
        }
        public void InsertOrder(string time, string id, string name, int count, string userid)
        {
            var Stored = "dbo.up_Common_MedicineOrder_InsertOrder";
            SqlParameter p1 = new SqlParameter("@Time", System.Data.SqlDbType.VarChar);
            p1.Value = time;
            SqlParameter p2 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
            p2.Value = id;
            SqlParameter p3 = new SqlParameter("@Name", System.Data.SqlDbType.VarChar);
            p3.Value = name;
            SqlParameter p4 = new SqlParameter("@Count", System.Data.SqlDbType.Int);
            p4.Value = count;
            SqlParameter p5 = new SqlParameter("@UserId", System.Data.SqlDbType.VarChar);
            p5.Value = userid;
            _dbContext.Database.ExecuteSqlCommand($"{Stored} @Time,@ID,@Name,@Count,@UserId", p1, p2, p3, p4, p5);
        }
    }
}
