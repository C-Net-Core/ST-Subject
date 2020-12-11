using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Models
{
    public class InfoOrderManagement
    {
        public int ID { get; set; }
        public int IDP { get; set; }
        public int IDSP { get; set; }
        public String NameProduct { get; set; }
        public String Amount { get; set; }
        public String Size { get; set; }
        public String Price { get; set; }
        public String IntoMoney { get; set; }
    }
}
