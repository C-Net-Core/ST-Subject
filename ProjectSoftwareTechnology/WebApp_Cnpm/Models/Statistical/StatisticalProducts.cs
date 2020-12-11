using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Models.Statistical
{
    public class StatisticalProducts
    {
        public int ID_Product { get; set; }
        public String Name_Product { get; set; }
        public int count_ProductSold {get;set;}
        public int totalMoney { get; set; }
        public String year { get; set; }
    }
}
