using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Models.Statistical
{
    public class StatisticalOrders
    {
        public int idMonth { get; set; }
        public int idYear { get; set; }
        public int totalOrders { get; set; }
        public int totalOrderSuccesses { get; set; }
        public int totalOrderCancels { get; set; }
        public string totalMoney { get; set; }
        public float ratioSuccess { get; set; }
        public float ratioCancel { get; set; }
    }
}
