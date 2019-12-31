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
        [StringLength(maximumLength:8, ErrorMessage = "Id must be 4-8 digit number", MinimumLength = 4)]
        public string ID { get; set; }
        [Required]
        [StringLength(maximumLength:16, ErrorMessage = "Name must be under 16 digit")]
        public string name { get; set; }
        [Required]
        [StringLength(maximumLength:16, ErrorMessage = "Origin must be under 16 digit")]
        public string origin { get; set; }
        [Required]
        [DataType(DataType.Date,ErrorMessage = "The date is not a valid date")]
        public string PD { get; set; }
        [Required]
        [StringLength(maximumLength:16, ErrorMessage = "EXP not rule")]
        public string EXP { get; set; }
        public string date { get; set; }
        public string price { get; set; }
        public string category { get; set; }
        public string usage { get; set; }
        public List<SelectListItem> Usage { get; set; }
    }
}
