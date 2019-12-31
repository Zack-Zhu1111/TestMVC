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
    public interface IInformationProvider
    {
        Task<List<InformationViewModel>> GetAllUserList();
        Task<InformationViewModel> GetUser(string id);
        void UpdateInfo(string id, string name, string sex, string age, string height, string weight);
    }
    public class InformationProvider : IInformationProvider
    {
        private readonly IInformationContext _db;
        private readonly DbContext _dbContext;
        public InformationProvider(IInformationContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public async Task<List<InformationViewModel>> GetAllUserList()
        {
            var storedProcedureName = "dbo.up_Common_Infomation_Select";
            try
            {
                var UserList = await _dbContext.Database.SqlQuery<InformationViewModel>($"{storedProcedureName}").ToListAsync();
                return UserList;
            }
            catch
            {
                return new List<InformationViewModel>();
            }
        }
        public async Task<InformationViewModel> GetUser(string id)
        {
            var storedProcedureName = "dbo.up_Common_Infomation_SelectByNumber";
            try
            {
                var selectedUser = await _dbContext.Database.SqlQuery<InformationViewModel>($"{storedProcedureName} @Number", new SqlParameter("@Number", id)).FirstOrDefaultAsync();
                return selectedUser;
            }
            catch
            {
                return new InformationViewModel();
            }
        }
        public void UpdateInfo(string id, string name, string sex, string age, string height, string weight)
        {
            var storedProcedureName = "dbo.up_Common_Information_InsertXXX";
            try
            {
                SqlParameter p1 = new SqlParameter("@Number", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                SqlParameter p2 = new SqlParameter("@Name", System.Data.SqlDbType.VarChar);
                p2.Value = name;
                SqlParameter p3 = new SqlParameter("@Sex", System.Data.SqlDbType.VarChar);
                p3.Value = sex;
                SqlParameter p4 = new SqlParameter("@Age", System.Data.SqlDbType.VarChar);
                p4.Value = age;
                SqlParameter p5 = new SqlParameter("@Height", System.Data.SqlDbType.VarChar);
                p5.Value = height;
                SqlParameter p6 = new SqlParameter("@Weight", System.Data.SqlDbType.VarChar);
                p6.Value = weight;
                _dbContext.Database.ExecuteSqlCommand($"{storedProcedureName} @Number,@Name,@Sex,@Age,@Height,@Weight", p1, p2, p3, p4, p5, p6);
            }
            catch
            {
                Console.Write("<script>alert('Update failed');history.go(-1);</script>");
            }
        }
    }
}
