using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Models
{
    public class CartItems
    {
        public int Idsp { get; set; }
        public String Img { get; set; }
        public String Name { get; set; }

        public String Size { get; set; }
        public String Cost { get; set; }
        public int Amount { get; set; }
        public int Total
        {
            get
            {
                return Amount * int.Parse(Cost);
            }
        }
    }
}
