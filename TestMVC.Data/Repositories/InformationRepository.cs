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
    public interface IInformationRepository
    {
        Task<List<InformationViewModel>> GetAllUserList();
        Task<InformationViewModel> GetUser(string id);
    }
    public class InformationRepository : IInformationRepository
    {
        private readonly IInformationContext _db;
        private readonly DbContext _dbContext;
        public InformationRepository(IInformationContext db)
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
                var selectedUser = await _dbContext.Database.SqlQuery<InformationViewModel>($"{storedProcedureName} @Number", new SqlParameter("@Number",id)).FirstOrDefaultAsync();
                return selectedUser;
            }
            catch
            {
                return new InformationViewModel();
            }
        }
    }
}
