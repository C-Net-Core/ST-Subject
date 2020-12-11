using Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cnpm.Models.Statistical;

namespace WebApp_Cnpm.Controllers.Handling
{
    public static class StatisticalCustommer
    {
        
        // sort 1
        public static List<StatisticalCustomers> statisticalCustomers_1(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }

                        if(order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderBy(d => d.totalOrder).ToList();
        }

        // sort 2
        public static List<StatisticalCustomers> statisticalCustomers_2(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderByDescending(d => d.totalOrder).ToList();
        }

        // sort 3
        public static List<StatisticalCustomers> statisticalCustomers_3(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderBy(d => d.totalOrderSuccess).ToList();
        }

        // sort 4
        public static List<StatisticalCustomers> statisticalCustomers_4(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderByDescending(d => d.totalOrderSuccess).ToList();
        }

        // sort 5
        public static List<StatisticalCustomers> statisticalCustomers_5(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderBy(d => d.totalOrderCancel).ToList();
        }

        // sort 6
        public static List<StatisticalCustomers> statisticalCustomers_6(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderByDescending(d => d.totalOrderCancel).ToList();
        }

        // sort 8
        public static List<StatisticalCustomers> statisticalCustomers_8(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderByDescending(d => d.totalMoney).ToList();
        }

        // sort 7
        public static List<StatisticalCustomers> statisticalCustomers_7(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderBy(d => d.totalMoney).ToList();
        }


        // sort 9
        public static List<StatisticalCustomers> statisticalCustomers_9(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderBy(d => d.ratio).ToList();
        }

        // sort 10
        public static List<StatisticalCustomers> statisticalCustomers_10(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam
            )
        {
            List<StatisticalCustomers> data = new List<StatisticalCustomers>();
            var total = orderService.GetAllPayOrder().Count();
            if (thang == null && nam == null)
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var timePay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && timePay == "2020")
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var timeCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && timeCancel == "2020")
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            else
            {
                foreach (var account in accountService.GetAllAccount())
                {
                    var totalOrder = 0;
                    var totalOrderSuccess = 0;
                    var totalOrderCancel = 0;
                    var totalMoney = 0;
                    foreach (var order in orderService.GetAllPayOrder())
                    {
                        // đếm đơn thành công
                        if (order.UID == account.UID && order.Status == "Đã giao hàng")
                        {
                            var monthPay = order.DateToPay.Split('-')[1];
                            var yearPay = order.DateToPay.Split('-')[2];
                            if (order.UID == account.UID && yearPay == nam && monthPay == thang)
                            {
                                totalOrderSuccess++;
                                totalMoney = int.Parse(order.Total);
                            }
                        }

                        // đếm đơn hủy
                        if (order.UID == account.UID && order.Status == "Đã hủy")
                        {
                            var monthCancel = order.DateCancel.Split('-')[1];
                            var yearCancel = order.DateCancel.Split('-')[2];
                            if (order.UID == account.UID && yearCancel == nam && monthCancel == thang)
                            {
                                totalOrderCancel++;
                            }
                        }
                        if (order.UID == account.UID)
                        {
                            // đếm tổng đơn
                            totalOrder++;
                        }
                    }

                    data.Add(new StatisticalCustomers
                    {
                        ID_Custommer = account.ID,
                        Name = account.UID,
                        totalOrder = totalOrder,
                        totalOrderSuccess = totalOrderSuccess,
                        totalOrderCancel = totalOrderCancel,
                        totalMoney = totalMoney,
                        ratio = ((float)totalOrderSuccess / (float)total) * 100
                    });
                }
            }
            return data.OrderByDescending(d => d.ratio).ToList();
        }

        public static List<StatisticalCustomers> control(
            IAccountDTOService accountService, IPayOrderDTOService orderService,
            string thang, string nam,int sort
            )
        {
            List<StatisticalCustomers> data = null;
            switch (sort)
            {
                case 1:
                    data = statisticalCustomers_1(accountService,orderService,thang,nam);
                    break;
                case 2:
                    data = statisticalCustomers_2(accountService, orderService, thang, nam);
                    break;
                case 3:
                    data = statisticalCustomers_3(accountService, orderService, thang, nam);
                    break;
                case 4:
                    data = statisticalCustomers_4(accountService, orderService, thang, nam);
                    break;
                case 5:
                    data = statisticalCustomers_5(accountService, orderService, thang, nam);
                    break;
                case 6:
                    data = statisticalCustomers_6(accountService, orderService, thang, nam);
                    break;
                case 7:
                    data = statisticalCustomers_7(accountService, orderService, thang, nam);
                    break;
                case 8:
                    data = statisticalCustomers_8(accountService, orderService, thang, nam);
                    break;
                case 9:
                    data = statisticalCustomers_9(accountService, orderService, thang, nam);
                    break;
                case 10:
                    data = statisticalCustomers_10(accountService, orderService, thang, nam);
                    break;
            }
            return data;
        }
    }
}
