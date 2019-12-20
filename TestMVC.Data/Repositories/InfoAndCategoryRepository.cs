using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVC.Domain.ViewModels;
using TestMVC.Data.Contexts;
using System.Data.Entity;
using System.Data.SqlClient;

namespace TestMVC.Data.Repositories
{
    public interface IInfoAndCategoryRepository
    {
        Task<List<InfoAndCategoryViewModel>> GetInfoAndCategoryAsync(string id, string category, string usage);
    }
    public class InfoAndCategoryRepository : IInfoAndCategoryRepository
    {
        private readonly IInfoAndCategoryContext _db;
        private readonly DbContext _dbContext;
        public InfoAndCategoryRepository(IInfoAndCategoryContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public async Task<List<InfoAndCategoryViewModel>> GetInfoAndCategoryAsync(string id, string category, string usage)
        {
            var storeProcedure = "dbo.up_Common_InfoMedicineAndMedicineCategory_SelectInfoAndCategory";
            try
            {
                var Info = await _dbContext.Database.SqlQuery<InfoAndCategoryViewModel>($"{storeProcedure} @ID,@Category,@Usage",
                    new SqlParameter("@ID", (id)), new SqlParameter("@Category", (category)), new SqlParameter("@Usage", (usage))).ToListAsync();
                return Info;
            }
            catch
            {
                return new List<InfoAndCategoryViewModel>();
            }
        }
    }
}
