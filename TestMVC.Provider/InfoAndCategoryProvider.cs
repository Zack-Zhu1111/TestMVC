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
    public interface IInfoAndCategoryProvider
    {
        List<InfoAndCategoryViewModel> GetInfoAndCategoryByUsage(string usage);
        List<InfoAndCategoryViewModel> GetInfoAndCategory();
        List<InfoAndCategoryViewModel> GetInfoAndCategoryByCategory(string category);
        List<InfoAndCategoryViewModel> GetInfoAndCategoryByCategoryAndUsage(string category, string usage);
        List<InfoAndCategoryViewModel> GetInfoAndCategorybyId(string id);
    }
    public class InfoAndCategoryProvider : IInfoAndCategoryProvider
    {
        private readonly IInfoAndCategoryContext _db;
        private readonly DbContext _dbContext;
        public InfoAndCategoryProvider(IInfoAndCategoryContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public  List<InfoAndCategoryViewModel> GetInfoAndCategoryByUsage(string usage)
        {
            var storeProcedure = "dbo.up_Common_InfoMedicineAndMedicineCategory_SelectByUsage";
            try
            {
                SqlParameter p1 = new SqlParameter("@Usage", System.Data.SqlDbType.VarChar);
                p1.Value = usage;
                var Info =  _dbContext.Database.SqlQuery<InfoAndCategoryViewModel>($"{storeProcedure} @Usage",p1).ToList();
                return Info;
            }
            catch
            {
                throw;
            }
        }
        public List<InfoAndCategoryViewModel> GetInfoAndCategory()
        {
            var storeProcedure = "dbo.up_Common_InfoMedicineAndMedicineCategory_Select";
            try
            {
                var Info = _dbContext.Database.SqlQuery<InfoAndCategoryViewModel>($"{storeProcedure}").ToList();
                return Info;
            }
            catch
            {
                throw;
            }
        }
        public List<InfoAndCategoryViewModel> GetInfoAndCategoryByCategory(string category)
        {
            var storeProcedure = "dbo.up_Common_InfoMedicineAndMedicineCategory_SelectByCategory";
            try
            {
                SqlParameter p1 = new SqlParameter("@Category", System.Data.SqlDbType.VarChar);
                p1.Value = category;
                var Info = _dbContext.Database.SqlQuery<InfoAndCategoryViewModel>($"{storeProcedure} @Category",p1).ToList();
                return Info;
            }
            catch
            {
                throw;
            }
        }
        public List<InfoAndCategoryViewModel> GetInfoAndCategoryByCategoryAndUsage(string category, string usage)
        {
            var storeProcedure = "dbo.up_Common_InfoMedicineAndMedicineCategory_SelectByCategoryAndUsage";
            try
            {
                SqlParameter p1 = new SqlParameter("@Category", System.Data.SqlDbType.VarChar);
                p1.Value = category;
                SqlParameter p2 = new SqlParameter("@Usage", System.Data.SqlDbType.VarChar);
                p2.Value = usage;
                var Info = _dbContext.Database.SqlQuery<InfoAndCategoryViewModel>($"{storeProcedure} @Category,@Usage", p1,p2).ToList();
                return Info;
            }
            catch
            {
                throw;
            }
        }
        public List<InfoAndCategoryViewModel> GetInfoAndCategorybyId(string id)
        {
            var storeProcedure = "dbo.up_Common_InfoMedicineAndMedicineCategory_SelectByID";
            try
            {
                SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                var Info = _dbContext.Database.SqlQuery<InfoAndCategoryViewModel>($"{storeProcedure} @ID", p1).ToList();
                return Info;
            }
            catch
            {
                throw;
            }
        }
    }
}
