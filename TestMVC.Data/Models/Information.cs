using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVC.Data.Models
{
    [Table("Information")]
    public class Information
    {
        [Key]
        public string number { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
    }
}
