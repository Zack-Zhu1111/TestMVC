using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVC.Domain.ViewModels
{
    public class ShoppingCartViewModel
    {
        public string UserId { get; set; }
        public string ID { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public int count { get; set; }
    }
}
