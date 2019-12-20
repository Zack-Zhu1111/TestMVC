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
        private readonly IMedicineUsageProvider _medicineusageprovider;
        private readonly IInfoMedicineProvider _infoMedicineProvider;
        private readonly IMedicineCategoryProvider _medicineCategoryProvider;
        private readonly IInfoAndCategoryProvider _infoAndCategoryProvider;
        public InfoMedicineController(IMedicineUsageProvider medicineusageprovider, IInfoMedicineProvider infoMedicineProvider, IMedicineCategoryProvider medicineCategoryProvider, IInfoAndCategoryProvider infoAndCategoryProvider)
        {
            _medicineusageprovider = medicineusageprovider;
            _infoMedicineProvider = infoMedicineProvider;
            _medicineCategoryProvider = medicineCategoryProvider;
            _infoAndCategoryProvider = infoAndCategoryProvider;
    }
        public ActionResult Main()
        {
            Session["status"] = "Information";
            if(Session["LoginUserName"] == null)
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
            Session["status"] = "Add Information";
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
                string EXP = Request.Form.Get("EXP");
                string price = Request.Form.Get("price");
                string category = Request.Form.Get("category");
                string usage = Request.Form.Get("Usage");
                _infoMedicineProvider.EditInfoMedicine(ID,name,origin,PD,EXP,price);
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

        public ActionResult Edit(string id)
        {
            Session["status"] = "Edit Information";
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
                    Model.EXP = SearchInfo[0].EXP;
                    Model.price = SearchInfo[0].price;
                   
                    var SearchCategory = _medicineCategoryProvider.GetCategoryByID(id);

                    Model.category = SearchCategory[0].category;
                    Model.usage = SearchCategory[0].usage;
                    
                    ViewData["Category"] = GetCategory();
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
            var EXP = Request.Form.Get("EXP");
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
       
    }
}