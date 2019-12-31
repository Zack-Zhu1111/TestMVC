using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVC.Domain.ViewModels;
using System.Data.Entity;
using TestMVC.Data.Contexts;
using System.Data.SqlClient;

namespace TestMVC.Provider
{
    public interface IInfoMedicineProvider
    {
        Task<InfoMedicineViewModel> GetInfoMedicineByIdAsync(string id);
        List<InfoMedicineViewModel> GetInfoMedicineById(string id);
        List<InfoMedicineViewModel> GetInfoMedicineAsync();
        List<InfoAndCategoryViewModel> GetAllInfo(string id,string category,string usage);
        void EditInfoMedicine(string id,string name,string origin,string PD,string EXP,string price);
        void DeleteInfoMedicine(string id);
    }
    public class InfoMedicineProvider : IInfoMedicineProvider
    {
        private readonly IInfoMedicineContext _db;
        private readonly DbContext _dbContext;
        public InfoMedicineProvider(IInfoMedicineContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public async Task<InfoMedicineViewModel> GetInfoMedicineByIdAsync(string id)
        {
            var medicineVm = await _db.InfoMedicine.Where(c => c.ID == id).FirstOrDefaultAsync();
            var searchVm = new InfoMedicineViewModel();
            if (medicineVm == null)
            {
                searchVm = null;
            }
            else
            {
                searchVm.ID = medicineVm.ID;
                searchVm.name = medicineVm.name;
                searchVm.origin = medicineVm.origin;
                searchVm.PD = medicineVm.PD;
                searchVm.EXP = medicineVm.EXP;
                searchVm.price = medicineVm.price;
            }
            return searchVm;
        }
        public List<InfoMedicineViewModel> GetInfoMedicineById(string id)
        {
            var storedProcedureName = "dbo.up_Common_InfoMedicine_SelectByID";
            try
            {
                SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                var list = _dbContext.Database.SqlQuery<InfoMedicineViewModel>($"{storedProcedureName} @ID",p1).ToList();
                return list;
            }
            catch
            {
                throw;
            }
           
        }
        public List<InfoMedicineViewModel> GetInfoMedicineAsync()
        {
            var stored = "dbo.up_Common_InfoMedicine_SelectXXX";
            try
            {
                var storelist = _dbContext.Database.SqlQuery<InfoMedicineViewModel>($"{stored}").ToList();
                return storelist;
            }
            catch
            {
                throw;
            }
        }
        public void EditInfoMedicine(string id, string name, string origin, string PD, string EXP, string price)
        {
            var storedProcedureName = "dbo.up_Common_InfoMedicine_InsertInfo";
            try
            {
                SqlParameter p1 = new SqlParameter("@ID",System.Data.SqlDbType.VarChar);
                p1.Value = id;
                SqlParameter p2 = new SqlParameter("@Name",System.Data.SqlDbType.VarChar);
                p2.Value = name;
                SqlParameter p3 = new SqlParameter("@Origin",System.Data.SqlDbType.VarChar);
                p3.Value = origin;
                SqlParameter p4 = new SqlParameter("@PD", System.Data.SqlDbType.VarChar);
                p4.Value = PD;
                SqlParameter p5 = new SqlParameter("@EXP",System.Data.SqlDbType.VarChar);
                p5.Value = EXP;
                SqlParameter p6 = new SqlParameter("@Price",System.Data.SqlDbType.VarChar);
                p6.Value = price;
                _dbContext.Database.ExecuteSqlCommand($"{storedProcedureName} @ID,@Name,@Origin,@PD,@EXP,@Price", p1, p2, p3, p4, p5, p6);
                
            }
            catch
            {
                throw;
            }
        }
        public void DeleteInfoMedicine(string id)
        {
            var storedProcedureName = "dbo.up_Common_InfoMedicine_Delete";
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
        public List<InfoAndCategoryViewModel> GetAllInfo(string id, string category, string usage)
        {
            var storedProcedureName = "dbo.up_Common_InfoMedicineAndMedicineCategory_SelectInfoAndCategory";
            try
            {
                SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                SqlParameter p2 = new SqlParameter("@Category", System.Data.SqlDbType.VarChar);
                p2.Value = category;
                SqlParameter p3 = new SqlParameter("@Usage", System.Data.SqlDbType.VarChar);
                p3.Value = usage;
                var list = _dbContext.Database.SqlQuery<InfoAndCategoryViewModel>($"{storedProcedureName} @ID, @Category, @Usage", p1, p2, p3).ToList();
                return list;
            }
            catch
            {
                throw;
            }
        }
    }
}
