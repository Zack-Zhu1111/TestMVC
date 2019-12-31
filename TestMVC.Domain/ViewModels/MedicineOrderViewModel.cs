using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVC.Domain.ViewModels
{
    public class MedicineOrderViewModel
    {
        public string time { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public string userid { get; set; }
    }
}
