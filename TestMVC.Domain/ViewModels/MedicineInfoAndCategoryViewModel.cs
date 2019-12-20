using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestMVC.Domain.ViewModels
{
    public class MedicineInfoAndCategoryViewModel
    {
        [Required]
        [StringLength(8, ErrorMessage = "Id not rule", MinimumLength = 4)]
        public string ID { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Name not rule")]
        public string name { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Origin not rule")]
        public string origin { get; set; }

        public string PD { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "EXP not rule")]
        public string EXP { get; set; }
        public string price { get; set; }
        public string category { get; set; }
        public string usage { get; set; }
        public List<SelectListItem> Usage { get; set; }
    }
}
