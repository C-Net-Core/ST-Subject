using Application.IServices;
using System.Collections.Generic;
using System.Linq;
using WebApp_Cnpm.Models.Statistical;

namespace WebApp_Cnpm.Controllers.Handling
{
    public static class StatisticalProcess
    {
        public static List<TurnoverOfYear> TurnoverOfYear(IPayOrderDTOService payOrderService,string sendYear)
        {
            List<TurnoverOfYear> data = new List<TurnoverOfYear>();
            var orders = payOrderService.GetAllPayOrder();
            // b1: duyệt tháng từ 1 -> 12
            for (int m = 1; m <= 12; m++)
            {
                var turnoverOfMonth = 0;
                var message = "";
                foreach (var order in orders)
                {
                    var dateToPay = order.DateToPay;
                    if (order.StatusPay == "Đã thanh toán")
                    {
                        if (dateToPay != null)
                        {
                            var ConvertDateToPay = dateToPay.Split('-');    // tách chuỗi ngày - tháng - năm
                            var month = ConvertDateToPay[1];    // lấy tháng ra
                            var year = ConvertDateToPay[2];    // lấy tháng ra

                            // kiểm tra tháng trùng khớp vs tháng duyệt ko
                            if (int.Parse(month) == m && year == sendYear)
                            {
                                // cộng dồn doanh thu của tháng đó
                                turnoverOfMonth += int.Parse(order.Total);
                            }
                        }
                    }
                }
                // lập thông báo cho tháng đó nếu doanh thu thấp hơn 1 triệu
                if (turnoverOfMonth < 1000000)
                {
                    message += "_";
                }
                // sau khi duyệt xong 1 tháng thì add vào mảng doanh thu của năm
                data.Add(new TurnoverOfYear
                {
                    Month = m.ToString(),
                    Year = sendYear,
                    Turnover = turnoverOfMonth.ToString(),
                    Message = message
                });
            }

            return data.OrderByDescending(d=>d.Turnover).ToList();
        }

        // đếm số lượng thông báo chỉ tiêu < 1 triệu
        public static int countTurnoverLess1Milion(IPayOrderDTOService payOrderService, string sendYear)
        {
            var less_1_million = TurnoverOfYear(payOrderService, sendYear).Where(t => t.Message == "_").ToList();
            if(less_1_million == null)
            {
                return 0;
            }
            return less_1_million.Count();
        }

        // đếm số lượng thông báo chỉ tiêu > 1 triệu
        public static int countTurnoverGreater1Milion(IPayOrderDTOService payOrderService, string sendYear)
        {
            var total = TurnoverOfYear(payOrderService, sendYear).Count();
            var less_1_million = TurnoverOfYear(payOrderService, sendYear).Where(t => t.Message == "_").ToList();
            if (less_1_million == null)
            {
                return total;
            }
            return total - less_1_million.Count();
        }

        // tổng doanh thu
        public static int countTotalStatistics(IPayOrderDTOService payOrderService, string sendYear)
        {
            return TurnoverOfYear(payOrderService, sendYear).Sum(t => int.Parse(t.Turnover));
        }

        // tháng có doanh thu cao nhất
        public static TurnoverOfYear HighestRevenue(IPayOrderDTOService payOrderService, string sendYear)
        {
            return TurnoverOfYear(payOrderService, sendYear).OrderByDescending(s => s.Turnover).First();
        }

        // tháng có doanh thu thấp nhất
        public static TurnoverOfYear LowestRevenue(IPayOrderDTOService payOrderService, string sendYear)
        {
            return TurnoverOfYear(payOrderService, sendYear).OrderBy(s => s.Turnover).First();
        }

    }
}
