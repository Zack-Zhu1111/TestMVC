using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVC.Data.Models
{
    
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        public string UserId { get; set; }
        public string ID { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public int count { get; set; }
    }
}
