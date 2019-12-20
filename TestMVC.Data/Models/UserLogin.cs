using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVC.Data.Models
{
    [Table("UserLogin")]
    public class UserLogin
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
