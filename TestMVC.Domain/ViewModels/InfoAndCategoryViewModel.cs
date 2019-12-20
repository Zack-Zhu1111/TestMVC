using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace TestMVC.Domain.ViewModels
{
    public class InfoAndCategoryViewModel
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string origin { get; set; }
        public string PD { get; set; }
        public string EXP { get; set; }
        public string price { get; set; }
        public string category { get; set; }
        public string usage { get; set; }
        public List<SelectListItem> Usage { get; set; }
    }
}
