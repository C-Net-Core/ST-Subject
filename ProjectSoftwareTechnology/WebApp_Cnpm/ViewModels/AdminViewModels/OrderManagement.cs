using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cnpm.Models;
using WebApp_Cnpm.Models.Helper;

namespace WebApp_Cnpm.ViewModels.AdminViewModels
{
    public class OrderManagement
    {
        public PaginatedList<PayOrderDTO> getOrderPage { get; set; }   // lấy ra ds đơn hàng có phân trang

        public List<InfoOrderManagement> detailOrders { get; set; }     // ds chi tiết đơn hàng

        public int ID_Order { get; set; }

        // count
        public int count_Watting_For_Progressing { get; set; }
        public int count_To_Ship { get; set; }
        public int count_To_Pay { get; set; }
        public int count_Delivered { get; set; }
        public int count_Cancel { get; set; }
        public int countAllOrder { get; set; }
        public String Search { get; set; }

    }
}
