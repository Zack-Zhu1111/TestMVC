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
    public interface IInfoMedicineRepository
    {
        Task<InfoMedicineViewModel> GetInfoMedicineByIdAsync(string id);
        Task<InfoMedicineViewModel> GetInfoMedicineAsync();
    }
    public class InfoMedicineRepository : IInfoMedicineRepository
    {
        private readonly IInfoMedicineContext _db;
        private readonly DbContext _dbContext;
        public InfoMedicineRepository(IInfoMedicineContext db)
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
                searchVm.ID = null;
                searchVm.name = null;
                searchVm.origin = null;
                searchVm.PD = null;
                searchVm.EXP = null;
                searchVm.price = null;
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
        public async Task<InfoMedicineViewModel> GetInfoMedicineAsync()
        {
            var medicineVm = await _db.InfoMedicine.FirstOrDefaultAsync();
            var searchVm = new InfoMedicineViewModel
            {
                ID = medicineVm.ID,
                name = medicineVm.name,
                origin = medicineVm.origin,
                PD = medicineVm.PD,
                EXP = medicineVm.EXP,
                price = medicineVm.price
            };
            return searchVm;
        }
    }
}
