using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVC.Domain.ViewModels;
using TestMVC.Data.Contexts;
using System.Data.Entity;
using TestMVC.Core;

namespace TestMVC.Provider
{
    public interface IUserLoginProvider
    {
        Task<UserLoginViewModel> GetUserLoginByUserNameAsync(string UserName);
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
            }
            else
            {
                LoginVm.UserName = userLoginVm.UserName;
                LoginVm.Password = userLoginVm.Password;
            }
            
            return LoginVm;
        }
    }
}
