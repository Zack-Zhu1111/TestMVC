using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVC.Domain.ViewModels
{
    public class InfoMedicineViewModel
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string origin { get; set; }
        public string PD { get; set; }
        public string EXP { get; set; }
        public string price { get; set; }
    }
}
