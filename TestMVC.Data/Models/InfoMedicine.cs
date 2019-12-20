using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVC.Data.Models
{
    [Table("InfoMedicine")]
    public class InfoMedicine
    {
        [Key]
        public string ID { get; set; }
        public string name { get; set; }
        public string origin { get; set; }
        public string PD { get; set; }
        public string EXP { get; set; }
        public string price { get; set; }
    }
}
