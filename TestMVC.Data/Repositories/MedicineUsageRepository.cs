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
    public interface IMedicineUsageRepository
    {
        List<MedicineUsageViewModel> GetMedicineUsage();
    }
    public class MedicineUsageRepository : IMedicineUsageRepository
    {
        private readonly IMedicineUsageContext _db;
        private readonly DbContext _dbContext;
        public MedicineUsageRepository(IMedicineUsageContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public List<MedicineUsageViewModel> GetMedicineUsage()
        {
            var storedProcedureName = "dbo.up_Common_MedicineUsage_Select";
            try
            {
                var usageList = _dbContext.Database.SqlQuery<MedicineUsageViewModel>($"{storedProcedureName}").ToList();
                return usageList;
            }
            catch
            {
                throw;
            }
        }
    }
}
