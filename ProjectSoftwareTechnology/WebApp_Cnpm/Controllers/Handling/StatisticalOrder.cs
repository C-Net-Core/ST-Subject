using Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cnpm.Models.Statistical;

namespace WebApp_Cnpm.Controllers.Handling
{
    public static class StatisticalOrder
    {
        public static List<StatisticalOrders> statisticalOrders(IPayOrderDTOService orderService, String nam)
        {
            List<StatisticalOrders> data = new List<StatisticalOrders>();

            for (int i = 1; i <= 12; i++)
            {
                var countOrders = 0;
                var countOrderSuccess = 0;
                var countOrderCancel = 0;
                var countMoney = 0;
                foreach (var order in orderService.GetAllPayOrder())
                {
                    if (order.Status == "Đã giao hàng")
                    {
                        var monthPay = int.Parse(order.DateToPay.Split('-')[1]);
                        var yearPay = order.DateToPay.Split('-')[2];
                        if (monthPay == i && yearPay == nam)
                        {
                            countOrderSuccess++;
                            countMoney += int.Parse(order.Total);
                            countOrders++;
                        }
                    }
                    if (order.Status == "Đã hủy")
                    {
                        var monthPay = int.Parse(order.DateCancel.Split('-')[1]);
                        var yearPay = order.DateCancel.Split('-')[2];
                        if (monthPay == i && yearPay == nam)
                        {
                            countOrderCancel++;
                            countOrders++;
                        }
                    }
                }
                if (countOrders == 0)
                {
                    data.Add(new StatisticalOrders
                    {
                        idMonth = i,
                        idYear = int.Parse(nam),
                        totalOrders = countOrders,
                        totalOrderSuccesses = countOrderSuccess,
                        totalOrderCancels = countOrderCancel,
                        totalMoney = countMoney.ToString(),
                        ratioSuccess = 0,
                        ratioCancel = 0,
                    });
                }
                else
                {
                    data.Add(new StatisticalOrders
                    {
                        idMonth = i,
                        idYear = int.Parse(nam),
                        totalOrders = countOrders,
                        totalOrderSuccesses = countOrderSuccess,
                        totalOrderCancels = countOrderCancel,
                        totalMoney = countMoney.ToString(),
                        ratioSuccess = ((float)countOrderSuccess / (float)countOrders) * 100,
                        ratioCancel = ((float)countOrderCancel / (float)countOrders) * 100,
                    });
                }
            }
            return data;
        }

        public static int totalOrders(IPayOrderDTOService orderService, String nam)
        {
            return statisticalOrders(orderService, nam).Sum(s => s.totalOrders);
        }

        public static int totalOrderSuccesses(IPayOrderDTOService orderService, String nam)
        {
            return statisticalOrders(orderService, nam).Sum(s => s.totalOrderSuccesses);
        }

        public static int totalOrderCancel(IPayOrderDTOService orderService, String nam)
        {
            return statisticalOrders(orderService, nam).Sum(s => s.totalOrderCancels);
        }

        public static int totalMoney(IPayOrderDTOService orderService, String nam)
        {
            return statisticalOrders(orderService, nam).Sum(s => int.Parse(s.totalMoney));
        }

        public static List<StatisticalOrders> statisticalOrders_Asc(IPayOrderDTOService orderService, String nam)
        {
            return statisticalOrders(orderService, nam).OrderBy(s => s.totalOrders).ToList();
        }

        public static List<StatisticalOrders> statisticalOrders_Dec(IPayOrderDTOService orderService, String nam)
        {
            return statisticalOrders(orderService, nam).OrderByDescending(s => s.totalOrders).ToList();
        }
    }
}
