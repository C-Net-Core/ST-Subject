using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Models.Statistical
{
    public class StatisticalCustomers
    {
        public int ID_Custommer { get; set; }
        public String Name { get; set; }
        public int totalOrder { get; set; }
        public int totalOrderSuccess { get; set; }
        public int totalOrderCancel { get; set; }
        public int totalMoney { get; set; }
        public float ratio { get; set; }   // tỉ lệ
    }
}
