using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMVC.Domain.ViewModels;
using TestMVC.Provider;
using TestMVC.Core;
using TestMVC.Data.Contexts;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TestMVC.Controllers
{
    public class InfoMedicineController : Controller
    {
        protected List<InfoAndCategoryViewModel> InfoMedicineList
        {
            get
            {
                if (Session["InfoMedicineList"] == null)
                { return null; }
                else
                { return Session["InfoMedicineList"] as List<InfoAndCategoryViewModel>; }
            }
            set { Session["InfoMedicineList"] = value; }
        }
        protected List<ShoppingCartViewModel> ShoppingList
        {
            get
            {
                if (Session["ShoppingList"] == null)
                { return null; }
                else
                { return Session["ShoppingList"] as List<ShoppingCartViewModel>; }
            }
            set { Session["ShoppingList"] = value; }
        }
        private readonly IMedicineUsageProvider _medicineusageprovider;
        private readonly IInfoMedicineProvider _infoMedicineProvider;
        private readonly IMedicineCategoryProvider _medicineCategoryProvider;
        private readonly IInfoAndCategoryProvider _infoAndCategoryProvider;
        private readonly IShoppingCartProvider _shoppingCartProvider;
        private readonly IMedicineOrderProvider _medicineOrderProvider;
        public InfoMedicineController(IMedicineUsageProvider medicineusageprovider, IInfoMedicineProvider infoMedicineProvider, IMedicineCategoryProvider medicineCategoryProvider,
            IInfoAndCategoryProvider infoAndCategoryProvider, IShoppingCartProvider shoppingCartProvider, IMedicineOrderProvider medicineOrderProvider)
        {
            _medicineusageprovider = medicineusageprovider;
            _infoMedicineProvider = infoMedicineProvider;
            _medicineCategoryProvider = medicineCategoryProvider;
            _infoAndCategoryProvider = infoAndCategoryProvider;
            _shoppingCartProvider = shoppingCartProvider;
            _medicineOrderProvider = medicineOrderProvider;
        }
        public ActionResult Main()
        {
            Session["status"] = "Medicine Information";
            if (Session["LoginUserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var viewModel = new InfoAndCategoryViewModel();
                viewModel.Usage = GetUsageListItemAsync();
                Search();
                return View(viewModel);
            }

        }
        public ActionResult Add()
        {
            //var viewModel = new InfoMedicineAdd();
            //viewModel.Usage = GetUsageListItemAsync();
            //return View(viewModel);
            Session["status"] = "Add Medicine Information";
            if (Session["LoginUserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            MedicineInfoAndCategoryViewModel model = new MedicineInfoAndCategoryViewModel();
            string Medicineno = Request.Form.Get("ID");
            model.Usage = GetUsageListItemAsync();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Add(string Medicineno)
        {
            Medicineno = Request.Form.Get("ID");
            var IdExist = await _infoMedicineProvider.GetInfoMedicineByIdAsync(Medicineno);
            if (IdExist != null)
            {
                return Content("<script>alert('This Medicine Number has been already added!');history.go(-1);</script>");
            }
            else
            {
                string ID = Medicineno;
                string name = Request.Form.Get("name");
                string origin = Request.Form.Get("origin");
                string PD = Request.Form.Get("PD");
                string EXP = Request.Form.Get("EXP") + Request.Form.Get("date");
                string price = Request.Form.Get("price");
                string category = Request.Form.Get("category");
                string usage = Request.Form.Get("Usage");
                _infoMedicineProvider.EditInfoMedicine(ID, name, origin, PD, EXP, price);
                _medicineCategoryProvider.EditCategory(ID, category, usage);
                return Content("<script>alert('Add Success!');window.location.href='Main';</script>");
            }
        }
        protected List<SelectListItem> GetUsageListItemAsync()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            List<MedicineUsageViewModel> list = _medicineusageprovider.GetMedicineUsage();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = list[i].usage.ToString();
                    item.Value = list[i].usage.ToString();
                    listItem.Add(item);
                }
            }
            return listItem;
        }

        [HttpPost]
        public ActionResult Search()
        {
            var Model = new List<InfoAndCategoryViewModel>();
            List<InfoAndCategoryViewModel> Infolist = new List<InfoAndCategoryViewModel>();
            string Id = null;
            string Category = null;
            string Usage = null;
            InfoMedicineContext dbMedicine = new InfoMedicineContext();
            MedicineCategoryContext dbCategory = new MedicineCategoryContext();
            if (Request.Form.Count > 0)
            {
                if ((Request.Form.Get("ID")) != "default")
                {
                    Id = Request.Form.Get("ID").ToString();
                }
                if ((Request.Form.Get("category")) != "default")
                {
                    Category = Request.Form.Get("category").ToString();
                }
                if ((Request.Form.Get("usage")) != "default")
                {
                    Usage = Request.Form.Get("usage").ToString();
                }
            }
            else
            {
                if (Session["ID"] != null)
                    Id = Session["ID"].ToString();
                if (Session["category"] != null)
                    Category = Session["category"].ToString();
                if (Session["usage"] != null)
                    Usage = Session["usage"].ToString();
            }
            if (Id == "")
                Id = null;
            if (Category == "--select--")
                Category = null;

            if (Id == null && Category == null && Usage == null)
            {
                Infolist = _infoAndCategoryProvider.GetInfoAndCategory();
                for (int i = 0; i < Infolist.Count; i++)
                {
                    InfoAndCategoryViewModel List = new InfoAndCategoryViewModel();
                    List.ID = Infolist[i].ID;
                    List.name = Infolist[i].name;
                    List.origin = Infolist[i].origin;
                    List.PD = Infolist[i].PD;
                    List.EXP = Infolist[i].EXP;
                    List.price = Infolist[i].price;
                    List.category = Infolist[i].category;
                    List.usage = Infolist[i].usage;
                    Model.Add(List);
                }
                ViewBag.length = Infolist.Count;
                System.Web.HttpContext.Current.Session["Category"] = null;
                System.Web.HttpContext.Current.Session["Usage"] = null;
            }
            else if (Id == null && Category == null && Usage != null)
            {
                Infolist = _infoAndCategoryProvider.GetInfoAndCategoryByUsage(Usage);
                for (int i = 0; i < Infolist.Count; i++)
                {
                    InfoAndCategoryViewModel List = new InfoAndCategoryViewModel();
                    List.ID = Infolist[i].ID;
                    List.name = Infolist[i].name;
                    List.origin = Infolist[i].origin;
                    List.PD = Infolist[i].PD;
                    List.EXP = Infolist[i].EXP;
                    List.price = Infolist[i].price;
                    List.category = Infolist[i].category;
                    List.usage = Infolist[i].usage;
                    Model.Add(List);
                }
                ViewBag.length = Infolist.Count;
                System.Web.HttpContext.Current.Session["Category"] = null;
                System.Web.HttpContext.Current.Session["Usage"] = Usage;
            }
            else if (Id == null && Category != null && Usage == null)
            {
                Infolist = _infoAndCategoryProvider.GetInfoAndCategoryByCategory(Category);
                for (int i = 0; i < Infolist.Count; i++)
                {
                    InfoAndCategoryViewModel List = new InfoAndCategoryViewModel();
                    List.ID = Infolist[i].ID;
                    List.name = Infolist[i].name;
                    List.origin = Infolist[i].origin;
                    List.PD = Infolist[i].PD;
                    List.EXP = Infolist[i].EXP;
                    List.price = Infolist[i].price;
                    List.category = Infolist[i].category;
                    List.usage = Infolist[i].usage;
                    Model.Add(List);
                }
                System.Web.HttpContext.Current.Session["Category"] = Category;
                System.Web.HttpContext.Current.Session["Usage"] = null;
            }
            else if (Id == null && Category != null && Usage != null)
            {
                Infolist = _infoAndCategoryProvider.GetInfoAndCategoryByCategoryAndUsage(Category, Usage);
                for (int i = 0; i < Infolist.Count; i++)
                {
                    InfoAndCategoryViewModel List = new InfoAndCategoryViewModel();
                    List.ID = Infolist[i].ID;
                    List.name = Infolist[i].name;
                    List.origin = Infolist[i].origin;
                    List.PD = Infolist[i].PD;
                    List.EXP = Infolist[i].EXP;
                    List.price = Infolist[i].price;
                    List.category = Infolist[i].category;
                    List.usage = Infolist[i].usage;
                    Model.Add(List);
                }
                System.Web.HttpContext.Current.Session["Category"] = Category;
                System.Web.HttpContext.Current.Session["Usage"] = Usage;
            }
            else if (Id != null)
            {
                Infolist = _infoAndCategoryProvider.GetInfoAndCategorybyId(Id);
                for (int i = 0; i < Infolist.Count; i++)
                {
                    InfoAndCategoryViewModel List = new InfoAndCategoryViewModel();
                    List.ID = Infolist[i].ID;
                    List.name = Infolist[i].name;
                    List.origin = Infolist[i].origin;
                    List.PD = Infolist[i].PD;
                    List.EXP = Infolist[i].EXP;
                    List.price = Infolist[i].price;
                    List.category = Infolist[i].category;
                    List.usage = Infolist[i].usage;
                    Model.Add(List);
                }
            }


            InfoMedicineList = Model;
            int pageindex = 1;
            var recordCount = InfoMedicineList.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 10;

            ViewBag.Medicine = InfoMedicineList.OrderBy(c => c.ID).Skip((pageindex - 1) * PAGE_SZ).Take(PAGE_SZ).ToList();
            ViewBag.Pager = new PagerHelper()
            {
                PageIndex = pageindex,
                PageSize = PAGE_SZ,
                RecordCount = recordCount
            };
            return View(Model);
        }
        public static SelectList GetCategory()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "OTC", Value = "OTC"},
                new SelectListItem() { Text = "RX", Value = "RX"}
            };
            SelectList Category = new SelectList(items, "Value", "Text");
            return Category;
        }
        public static SelectList GetDate()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "days", Value = "days"},
                new SelectListItem() { Text = "months", Value = "months"},
                new SelectListItem() { Text = "years", Value = "years"}
            };
            SelectList Date = new SelectList(items, "Value", "Text");
            return Date;
        }

        public ActionResult Edit(string id)
        {
            Session["status"] = "Edit Medicine Information";
            try
            {
                if (Session["LoginUserName"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var Model = new MedicineInfoAndCategoryViewModel();
                var SearchInfo = _infoMedicineProvider.GetInfoMedicineById(id);

                Model.Usage = GetUsageListItemAsync();

                Model.ID = SearchInfo[0].ID;
                Model.name = SearchInfo[0].name;
                Model.origin = SearchInfo[0].origin;
                Model.PD = SearchInfo[0].PD;
                string a = "[0-9]+";
                Regex regex = new Regex(a);
                Match match = regex.Match(SearchInfo[0].EXP);
                Model.EXP = match.ToString();
                string b = "[a-z]+";
                Regex Regex = new Regex(b);
                Match Match = Regex.Match(SearchInfo[0].EXP);
                Model.date = Match.ToString();
                Model.price = SearchInfo[0].price;

                var SearchCategory = _medicineCategoryProvider.GetCategoryByID(id);

                Model.category = SearchCategory[0].category;
                Model.usage = SearchCategory[0].usage;

                ViewData["Category"] = GetCategory();
                ViewData["Date"] = GetDate();
                return View(Model);


            }
            catch
            {
                return RedirectToAction("Main", "InfoMedicine");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Edit()
        {
            var id = Request.Form.Get("ID");
            var name = Request.Form.Get("name");
            var origin = Request.Form.Get("origin");
            var PD = Request.Form.Get("PD");
            var EXP = Request.Form.Get("EXP") + Request.Form.Get("date");
            var price = Request.Form.Get("price");
            var category = Request.Form.Get("category");
            var usage = Request.Form.Get("usage");
            _infoMedicineProvider.EditInfoMedicine(id, name, origin, PD, EXP, price);
            _medicineCategoryProvider.EditCategory(id, category, usage);
            return Content("<script>alert('Edit Success!');window.location.href='Main';</script>");

        }

        public ActionResult Delete(string id)
        {
            _infoMedicineProvider.DeleteInfoMedicine(id);
            _medicineCategoryProvider.DeleteCategory(id);
            var result = new InfoMedicineDeleteViewModel();
            result.ErrorMessage = "Delete Success!";
            return Json(result);
        }

        public ActionResult UserMain()
        {
            Session["status"] = "Medicine Information";
            if (Session["LoginUserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var viewModel = new InfoAndCategoryViewModel();
                viewModel.Usage = GetUsageListItemAsync();
                Search();
                return View(viewModel);
            }
        }

        public ActionResult Buy(string id)
        {
            var model = _infoMedicineProvider.GetInfoMedicineById(id);
            var IdIsExist = _shoppingCartProvider.GetById(id);
            if (IdIsExist == null)
            {
                var userid = Session["LoginId"].ToString();
                var ID = model[0].ID;
                var name = model[0].name;
                var price = model[0].price;
                var count = 1;
                _shoppingCartProvider.InsertShopping(userid, ID, name, price, count);
            }
            else
            {
                var userid = Session["LoginId"].ToString();
                var ID = model[0].ID;
                var name = model[0].name;
                var Price = Convert.ToInt32(IdIsExist[0].price) + Convert.ToInt32(model[0].price);
                var count = IdIsExist[0].count + 1;
                string price = Convert.ToString(Price);
                _shoppingCartProvider.InsertShopping(userid, ID, name, price, count);
            }
            var result = new InfoMedicineDeleteViewModel();
            result.ErrorMessage = "The medicine has been added in the cart!";
            return Json(result);
        }
        public ActionResult ShoppingCart()
        {
            Session["status"] = "Shopping Cart";
            if (Session["LoginUserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var viewModel = new List<ShoppingCartViewModel>();
            var userid = Session["LoginId"].ToString();
            var model = _shoppingCartProvider.GetByUserId(userid);
            int total = 0;
            if (model != null)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    ShoppingCartViewModel list = new ShoppingCartViewModel();
                    list.ID = model[i].ID;
                    list.name = model[i].name;
                    list.price = model[i].price;
                    list.count = model[i].count;
                    viewModel.Add(list);
                    total = total + Convert.ToInt32(list.price);
                }
            }
            ViewBag.total = total;
            ShoppingList = viewModel;
            ViewBag.ShoppingCart = ShoppingList.OrderBy(c => c.ID).ToList();
            return View(viewModel);
        }
        public ActionResult AddCount(string id)
        {
            var model = _shoppingCartProvider.GetById(id);
            string userid = model[0].UserId;
            string name = model[0].name;
            int Count = model[0].count + 1;
            int Price = Convert.ToInt32(model[0].price) * Count / model[0].count;
            string price = Price.ToString();
            _shoppingCartProvider.InsertShopping(userid, id, name, price, Count);
            return RedirectToAction("ShoppingCart", "InfoMedicine");
        }
        public ActionResult ReduceCount(string id)
        {
            var model = _shoppingCartProvider.GetById(id);
            string userid = model[0].UserId;
            string name = model[0].name;
            if (model[0].count >= 2)
            {
                int Count = model[0].count - 1;
                int Price = Convert.ToInt32(model[0].price) * Count / model[0].count;
                string price = Price.ToString();
                _shoppingCartProvider.InsertShopping(userid, id, name, price, Count);
                return RedirectToAction("ShoppingCart", "InfoMedicine");
            }
            else
            {
                return RedirectToAction("ShoppingCart", "InfoMedicine");
            }
        }
        public ActionResult DeleteShopping(string id)
        {
            //var model = _shoppingCartProvider.GetById(id);
            _shoppingCartProvider.DeleteShopping(id);
            return RedirectToAction("ShoppingCart", "InfoMedicine");
        }
        public ActionResult GoBack()
        {
            if (Session["Power"].ToString() == "Manager")
            {
                return RedirectToAction("Main", "InfoMedicine");
            }
            else
            {
                return RedirectToAction("UserMain", "InfoMedicine");
            }
        }

        [HttpPost]
        public ActionResult BuyShopping()
        {
            var Name = Request.Form.Get("name");
            if (Name == "")
                Name = null;
            var Address = Request.Form.Get("address");
            if (Address == "")
                Address = null;
            if (Name != null && Address != null)
            {
                var userid = Session["LoginId"].ToString();
                var model = _shoppingCartProvider.GetByUserId(userid);
                for(int i = 0; i < model.Count; i++)
                {
                    string time = DateTime.Now.ToString();
                    string id = model[i].ID;
                    string name = model[i].name;
                    int count = model[i].count;
                    _medicineOrderProvider.InsertOrder(time, id, name, count, userid);
                }
                _shoppingCartProvider.DeleteUser(userid);
                return Content("<script>alert('buy Success!');window.location.href='UserMain';</script>");
            }
            else
            {
                return Content("<script>alert('Please fill out your name and address');history.go(-1);</script>");
            }
        }
        public ActionResult Order()
        {
            Session["status"] = "Order";
            if (Session["LoginUserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userid = Session["LoginId"].ToString();
            var model = _medicineOrderProvider.GetOrder(userid);
            var viewModel = new List<MedicineOrderViewModel>();
            ViewBag.Order = model.OrderByDescending(c => c.time).ToList();
            return View(model);
        }
        public ActionResult DeleteOrder(string time)
        {
            string userid = Session["LoginId"].ToString();
            _medicineOrderProvider.DeleteOrder(time,userid);
            return RedirectToAction("Order", "InfoMedicine");
        }
        public ActionResult DeleteAll()
        {
            string userid = Session["LoginId"].ToString();
            _medicineOrderProvider.DeleteAll(userid);
            return RedirectToAction("Order", "InfoMedicine");
        }
    }
}