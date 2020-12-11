
using Application.IServices;
using System.Collections.Generic;
using System.Linq;
using WebApp_Cnpm.Models.Statistical;

namespace WebApp_Cnpm.Controllers.Handling
{
    public static class StatisticalProduct
    {
        public static List<StatisticalProducts> TopSoldProducts_Greater(
                IProductDTOService productService, IInfoOrderDTOService infoOrderService,
                IPayOrderDTOService payOrderService,
                string thang,string nam
            )
        {
            List<StatisticalProducts> data = new List<StatisticalProducts>();

            var products = productService.GetAllProduct();
            var payOrders = payOrderService.GetAllPayOrder();
            var infoOrders = infoOrderService.GetAllInfoOrder();
            foreach (var product in products)
            {
                var countAmount_Product_Sold = 0;
                var totalMoney = 0;
                foreach (var pay in payOrders)
                {
                    if (pay.StatusPay == "Đã thanh toán")
                    {
                        var dateToPay = pay.DateToPay;
                        var ConvertDateToPay = dateToPay.Split('-');    // tách chuỗi ngày - tháng - năm
                        var month = ConvertDateToPay[1];
                        var year = ConvertDateToPay[2];    // lấy tháng năm
                        if (nam == year && thang == month)
                        {
                            foreach (var info in infoOrders)
                            {
                                if (info.IDP == pay.ID && info.IDSP == product.IDSP)
                                {
                                    countAmount_Product_Sold += int.Parse(info.Amount);
                                    totalMoney += int.Parse(info.IntoMoney);
                                }
                            }
                        }
                    }
                }
                data.Add(new StatisticalProducts { 
                    ID_Product = product.IDSP,
                    Name_Product = product.Name,
                    count_ProductSold = countAmount_Product_Sold,
                    totalMoney = totalMoney,
                    year = nam,
                });
            }
            return data.OrderByDescending(d=>d.count_ProductSold).ToList();
        }

        // sl sp đã bán
        public static int countProductSold(IProductDTOService productService, IInfoOrderDTOService infoOrderService,
                IPayOrderDTOService payOrderService,
                string thang,string nam)
        {
            return TopSoldProducts_Greater(productService, infoOrderService, payOrderService,thang, nam)
                                            .Sum(p => p.count_ProductSold);
        }

        //  tổng tiền thu được từ sp đã bán
        public static int countMoney_ProductSold(IProductDTOService productService, IInfoOrderDTOService infoOrderService,
                IPayOrderDTOService payOrderService,
                string thang,string nam)
        {
            return TopSoldProducts_Greater(productService, infoOrderService, payOrderService,thang, nam)
                                            .Sum(p => p.totalMoney);
        }

        // ======================== TRONG NĂM ================================================

        public static List<StatisticalProducts> TopSoldProducts_GreaterInYear(
                IProductDTOService productService, IInfoOrderDTOService infoOrderService,
                IPayOrderDTOService payOrderService,
                string nam
            )
        {
            List<StatisticalProducts> data = new List<StatisticalProducts>();

            var products = productService.GetAllProduct();
            var payOrders = payOrderService.GetAllPayOrder();
            var infoOrders = infoOrderService.GetAllInfoOrder();
            foreach (var product in products)
            {
                var countAmount_Product_Sold = 0;
                var totalMoney = 0;
                foreach (var pay in payOrders)
                {
                    if (pay.StatusPay == "Đã thanh toán")
                    {
                        var dateToPay = pay.DateToPay;
                        var ConvertDateToPay = dateToPay.Split('-');    // tách chuỗi ngày - tháng - năm
                        var year = ConvertDateToPay[2];    // lấy tháng năm
                        if (nam == year)
                        {
                            foreach (var info in infoOrders)
                            {
                                if (info.IDP == pay.ID && info.IDSP == product.IDSP)
                                {
                                    countAmount_Product_Sold += int.Parse(info.Amount);
                                    totalMoney += int.Parse(info.IntoMoney);
                                }
                            }
                        }
                    }
                }
                data.Add(new StatisticalProducts
                {
                    ID_Product = product.IDSP,
                    Name_Product = product.Name,
                    count_ProductSold = countAmount_Product_Sold,
                    totalMoney = totalMoney,
                    year = nam,
                });
            }
            return data.OrderByDescending(d => d.count_ProductSold).ToList();
        }

        // sl sp đã bán
        public static int countProductSoldInYear(IProductDTOService productService, IInfoOrderDTOService infoOrderService,
                IPayOrderDTOService payOrderService,
                string nam)
        {
            return TopSoldProducts_GreaterInYear(productService, infoOrderService, payOrderService, nam)
                                            .Sum(p => p.count_ProductSold);
        }

        //  tổng tiền thu được từ sp đã bán
        public static int countMoney_ProductSoldInYear(IProductDTOService productService, IInfoOrderDTOService infoOrderService,
                IPayOrderDTOService payOrderService,
               string nam)
        {
            return TopSoldProducts_GreaterInYear(productService, infoOrderService, payOrderService, nam)
                                            .Sum(p => p.totalMoney);
        }
    }
}
