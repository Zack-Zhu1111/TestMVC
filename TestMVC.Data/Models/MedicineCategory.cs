using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVC.Data.Models
{
    [Table("MedicineCategory")]
    public class MedicineCategory
    {
        [Key]
        public string ID { get; set; }
        public string category { get; set; }
        public string usage { get; set; }
    }
}
