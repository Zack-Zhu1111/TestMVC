using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVC.Domain.ViewModels;
using TestMVC.Data.Contexts;
using System.Data.Entity;

namespace TestMVC.Data.Repositories
{
    public interface IMedicineCategoryRepository
    {
        Task<MedicineCategoryViewModel> GetCategoryByIDAsync(string id);
        Task<MedicineCategoryViewModel> GetCategoryByCategoryAsync(string Category);
        Task<MedicineCategoryViewModel> GetCategoryByUsageAsync(string Usage);
    }
    public class MedicineCategoryRepository : IMedicineCategoryRepository
    {
        private readonly MedicineCategoryContext _db;
        private readonly DbContext _dbContext;
        public MedicineCategoryRepository(MedicineCategoryContext db)
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
    }
}
