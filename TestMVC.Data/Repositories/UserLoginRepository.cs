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
    public interface IUserLoginRepository
    {
        Task<UserLoginViewModel> GetUserLoginByUserNameAsync(string UserName);
        //Task<UserLoginViewModel> GetUser();
    }
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly IUserLoginContext _db;
        public UserLoginRepository(IUserLoginContext db)
        {
            _db = db;
        }
        public async Task<UserLoginViewModel> GetUserLoginByUserNameAsync(string loginID)
        {
            var userLoginVm = await _db.UserLogin.Where(c => c.UserName == loginID).FirstOrDefaultAsync();
            var LoginVm = new UserLoginViewModel
            {
                UserName = userLoginVm.UserName,
                Password = userLoginVm.Password
            };
            return LoginVm;
        }
        //public async Task<UserLoginViewModel> GetUser()
        //{
        //    var UserVM = await _db.UserLogin.
        //}
    }
}
