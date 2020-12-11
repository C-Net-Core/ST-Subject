using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Models
{
    public class DetailOrders
    {
        public String NameCustomer { get; set; }
        public String PhoneCustomer { get; set; }
        public String AddressCustomer { get; set; }
        public int ID_PayOrder { get; set; }
        public String DateOrder { get; set; }
        public String ImageProduct { get; set; }
        public String NameProduct { get; set; }
        public String PriceProduct { get; set; }
        public String SizeProduct { get; set; }
        public int AmountProduct { get; set; }
        public String StatusOrder { get; set; }
        public int SubTotal{ get; set; }
        public int FeeShip { get; set; }
        public int Total { get; set; }

    }
}
