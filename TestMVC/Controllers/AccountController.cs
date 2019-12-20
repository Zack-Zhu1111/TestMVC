using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using TestMVC.Provider;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TestMVC.Domain.ViewModels;

namespace TestMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserLoginProvider _UserNameProvider;
        private readonly IInformationProvider _informationProvider;
        private static int ActionYear => string.IsNullOrEmpty(ConfigurationManager.AppSettings["ActionYear"]) ? DateTime.UtcNow.Year : ConfigurationManager.AppSettings["ActionYear"].FromJson<int>();
        
        public AccountController(IUserLoginProvider userNameProvider, IInformationProvider informationProvider)
        {
            _UserNameProvider = userNameProvider;
            _informationProvider = informationProvider;
        }
        public ActionResult Login()
        {
            ViewBag.ActionYear = ActionYear;
            Session["Year"] = ActionYear;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
       public async Task<ActionResult> Login(string user)
        {
            user = Request.Form["login"];
            var Id = await _UserNameProvider.GetUserLoginByUserNameAsync(user);
            if (Id.UserName != null)
            {
                if (int.Parse(Id.Password) == int.Parse(Request.Form["password"]))
                {
                    var model = await _informationProvider.GetUser(user);
                    Session["LoginUserName"] = model.name;
                    return RedirectToAction("Main", "InfoMedicine");
                }
                else
                {
                    ViewBag.ErrorMessage = "Wrong Password";
                }
            }
            else
            {
                ViewBag.Error = "Wrong UserID";
            }
            return View();
        }
        
    }
}