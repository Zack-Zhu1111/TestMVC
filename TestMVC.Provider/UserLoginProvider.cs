using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVC.Domain.ViewModels;
using TestMVC.Data.Contexts;
using System.Data.Entity;
using TestMVC.Core;
using System.Data.SqlClient;

namespace TestMVC.Provider
{
    public interface IUserLoginProvider
    {
        Task<UserLoginViewModel> GetUserLoginByUserNameAsync(string UserName);
        void UpdatePasswordAsync(string id, string pwd);
    }
    public class UserLoginProvider : IUserLoginProvider
    {
        private readonly IUserLoginContext _db;
        private readonly DbContext _dbContext;
        private static string assemblyInfo = AssemblyHelper.GetFormattedAssemblyInfo(typeof(IUserLoginProvider));
        public UserLoginProvider(IUserLoginContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public async Task<UserLoginViewModel> GetUserLoginByUserNameAsync(string loginID)
        {
            var userLoginVm = await _db.UserLogin.Where(c => c.UserName == loginID).FirstOrDefaultAsync();
            var LoginVm = new UserLoginViewModel();
            if (userLoginVm == null)
            {
                LoginVm.UserName = null;
                LoginVm.Password = null;
                LoginVm.Power = null;
            }
            else
            {
                LoginVm.UserName = userLoginVm.UserName;
                LoginVm.Password = userLoginVm.Password;
                LoginVm.Power = userLoginVm.Power;
            }
            
            return LoginVm;
        }
        public void UpdatePasswordAsync(string id, string pwd)
        {
            var storedProcedureName = "dbo.up_Common_UserLogin_UpdatePassword";
            try
            {
                SqlParameter p1 = new SqlParameter("@UserName", System.Data.SqlDbType.VarChar);
                p1.Value = id;
                SqlParameter p2 = new SqlParameter("@Password", System.Data.SqlDbType.VarChar);
                p2.Value = pwd;
                _dbContext.Database.ExecuteSqlCommand($"{storedProcedureName} @UserName,@Password", p1, p2);
            }
            catch
            {

            }
        }
    }
}
