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
            HttpCookie httpCookie = Request.Cookies.Get("Login");
            if (httpCookie != null)
            {
                ViewBag.LoginUser = httpCookie.Values["LoginUser"].ToString();
                ViewBag.LoginPwd = httpCookie.Values["LoginPwd"].ToString();
            }
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
                    var ck = Request.Form["checkbox"];
                    if(ck == "on")
                    {
                        HttpCookie cookie = new HttpCookie("Login");
                        cookie["LoginUser"] = Id.UserName;
                        cookie["LoginPwd"] = Id.Password;
                        cookie.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie);
                    }
                    Session["LoginId"] = user;
                    Session["Password"] = Id.Password;
                    Session["LoginUserName"] = model.name;
                    Session["Power"] = Id.Power;
                    if(Id.Power == "Manager")
                    {
                        return RedirectToAction("Main", "InfoMedicine");
                    }
                    else
                    {
                        return RedirectToAction("UserMain", "InfoMedicine");
                    }
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
        public ActionResult Modify()
        {
            if (Session["LoginUserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Session["status"] = "Modify Password";
            ViewBag.power = Session["Power"].ToString();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Modify(string id)
        {
            id = Session["LoginId"].ToString();
            string Loginpwd = Session["Password"].ToString();
            string pwd = Request.Form["Newpwd"];
            string Cpwd = Request.Form["CNewpwd"];
            if(pwd == Loginpwd)
            {
                return Content("<script>alert('Your new password has no change');history.go(-1);</script>");
            }
            if(pwd == Cpwd)
            {
                _UserNameProvider.UpdatePasswordAsync(id, pwd);
                HttpCookie httpCookie = Request.Cookies.Get("Login");
                httpCookie["LoginPwd"] = null;
                Response.Cookies["LoginPwd"].Expires = DateTime.Now.AddMinutes(-1);
                Response.Cookies.Add(httpCookie);
                return Content("<script>alert('Update success!');window.location.href='Login';</script>");
            }
            else
            {
                return Content("<script>alert('Update failed');history.go(-1);</script>");
            }
        }
        public async Task<ActionResult> Edit()
        {
            if (Session["LoginUserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Session["status"] = "Edit Personal Information";
            ViewBag.power = Session["Power"].ToString();
            InformationViewModel model = new InformationViewModel();
            var id = Session["LoginId"].ToString();
            model = await _informationProvider.GetUser(id);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Edit(InformationViewModel Model)
        {
            string id = Model.number;
            string name = Model.name;
            string sex = Model.sex;
            string age = Model.age;
            string height = Model.height;
            string weight = Model.weight;
            _informationProvider.UpdateInfo(id,name,sex,age,height,weight);
            if (Session["Power"].ToString() == "Manager")
            {
                return RedirectToAction("Main", "InfoMedicine");
            }
            else
            {
                return RedirectToAction("UserMain", "InfoMedicine");
            }
        }
    }
}