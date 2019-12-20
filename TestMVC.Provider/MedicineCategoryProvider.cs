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
    public interface IMedicineCategoryProvider
    {
        Task<MedicineCategoryViewModel> GetCategoryByIDAsync(string id);
        Task<MedicineCategoryViewModel> GetCategoryByCategoryAsync(string Category);
        Task<MedicineCategoryViewModel> GetCategoryByUsageAsync(string Usage);
        List<MedicineCategoryViewModel> GetCategory();
        List<MedicineCategoryViewModel> GetCategoryByID(string id);
        void EditCategory(string id, string category, string usage);
        void DeleteCategory(string id);
    }
    public class MedicineCategoryProvider : IMedicineCategoryProvider
    {
        private readonly IMedicineCategoryContext _db;
        private readonly DbContext _dbContext;
        public MedicineCategoryProvider(IMedicineCategoryContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public async Task<MedicineCategoryViewModel> GetCategoryByIDAsync(string id)
        {
            var categoryVm = await _db.MedicineCategory.Where(c => c.ID == id).FirstOrDefaultAsync();
            var searchVm = new MedicineCategoryViewModel
            {
                ID = categoryVm.ID,
                category = categoryVm.category,
                usage = categoryVm.usage
            };
            return searchVm;
        }
        public async Task<MedicineCategoryViewModel> GetCategoryByCategoryAsync(string Category)
        {
            var categoryVm = await _db.MedicineCategory.Where(c => c.category == Category).FirstOrDefaultAsync();
            var searchVm = new MedicineCategoryViewModel
            {
                ID = categoryVm.ID,
                category = categoryVm.category,
                usage = categoryVm.usage
            };
            return searchVm;
        }
        public async Task<MedicineCategoryViewModel> GetCategoryByUsageAsync(string Usage)
        {
            var categoryVm = await _db.MedicineCategory.Where(c => c.usage == Usage).FirstOrDefaultAsync();
            var searchVm = new MedicineCategoryViewModel
            {
                ID = categoryVm.ID,
                category = categoryVm.category,
                usage = categoryVm.usage
            };
            return searchVm;
        }
        public List<MedicineCategoryViewModel> GetCategory()
        {
            var stored = "dbo.up_Common_MedicineCategory_Select";
            try
            {
                var categoryList = _dbContext.Database.SqlQuery<MedicineCategoryViewModel>($"{stored}").ToList();
                return categoryList;
            }
            catch
            {
                throw;
            }
        }
        public void EditCategory(string id, string category, string usage)
        {
            var storedProcedureName = "dbo.up_Common_MedicineCategory_InsertInfo";
            try
            {
                SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                SqlParameter p2 = new SqlParameter("@Category", System.Data.SqlDbType.VarChar);
                p2.Value = category;
                SqlParameter p3 = new SqlParameter("@Usage", System.Data.SqlDbType.VarChar);
                p3.Value = usage;
                _dbContext.Database.ExecuteSqlCommand($"{storedProcedureName} @ID, @Category, @Usage", p1, p2, p3);
            }
            catch
            {

            }
        }
        public void DeleteCategory(string id)
        {
            var storedProcedureName = "dbo.up_Common_MedicineCategory_Delete";
            try
            {
                SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                _dbContext.Database.ExecuteSqlCommand($"{storedProcedureName} @ID", p1);
            }
            catch
            {

            }
        }
        public List<MedicineCategoryViewModel> GetCategoryByID(string id)
        {
            var storedProcedureName = "dbo.up_Common_MedicineCategory_SelectByID";
            try
            {
                SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                var list = _dbContext.Database.SqlQuery<MedicineCategoryViewModel>($"{storedProcedureName} @ID", p1).ToList();
                return list;
            }
            catch
            {
                throw;
            }
        }
    }
}
