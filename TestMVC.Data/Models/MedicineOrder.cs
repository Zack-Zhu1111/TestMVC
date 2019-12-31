using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVC.Data.Models
{
    [Table("MedicineOrder")]
    public class MedicineOrder
    {
        [Key]
        public string time { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public string userid { get; set; }
    }
}
