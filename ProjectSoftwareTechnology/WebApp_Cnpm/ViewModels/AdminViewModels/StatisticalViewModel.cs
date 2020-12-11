using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cnpm.Models.Statistical;

namespace WebApp_Cnpm.ViewModels.AdminViewModels
{
    public class StatisticalViewModel
    {
        #region Turnover
        public List<TurnoverOfYear> TurnoverOfYears { get; set; }
        public int countTurnoverGreater1Milion { get; set; }   // > 1 triệu
        public int countTurnoverLess1Milion { get; set; }   // < 1 triệu
        public int countTotalStatistics { get; set; }
        public TurnoverOfYear HighestRevenue { get; set; }
        public TurnoverOfYear LowestRevenue { get; set; }
        #endregion

        #region Statistical Product
        public List<StatisticalProducts> TopSoldProducts_Greater { get; set; }
        public int countProductSold { get; set; }
        public int countMoney_ProductSold { get; set; }
        #endregion

        #region Statistical Custommer
        public List<StatisticalCustomers> StatisticalCustomers { get; set; }
        #endregion

        #region Order Statistical
        public List<StatisticalOrders> StatisticalOrders { get; set; }
        public int totalOrders { get; set; }
        public int totalOrderSuccesses { get; set; }
        public int totalOrderCancel { get; set; }
        public int totalMoney { get; set; }
        #endregion

        #region common
        public string month { get; set; }
        public string year { get; set; }
        #endregion
    }
}
