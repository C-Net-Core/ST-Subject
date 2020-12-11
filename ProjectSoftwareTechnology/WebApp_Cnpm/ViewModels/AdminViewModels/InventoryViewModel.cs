using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cnpm.Models.Helper;

namespace WebApp_Cnpm.ViewModels.AdminViewModels
{
    public class InventoryViewModel
    {
        public IEnumerable<ProductDTO> productDTOs { get; set; }        // lấy ra tất cả sp
        public PaginatedList<ProductDTO> AllProduct { get; set; }       // lấy ra tất cả sp có phân trang
        public PaginatedList<ProductDTO> AmountProducts { get; set; }   // lấy ra tất cả sp có phân trang theo search
        public int countAmountProductWhere { get; set; }                // đếm số lượng sp sau khi search
        public IEnumerable<SaleDTO> Sales { get; set; }                 // lấy ra ds sale
        public int countProducts { get; set; }                          // đếm tổng số lượng của tất cả sp
        public String Search { get; set; }                              // giá trị tìm kiếm
        public int AllDeliver { get; set; }                             // tổng sl sp đã xuất kho
        public PaginatedList<ToDelivered> Delivereds { get; set; }        // ds kiểm kê còn lại bao nhiu sp

        // a lot of fields of to receive
        public String idToReceive { get; set; }
        public String currentQuantity { get; set; }
        public String amountToReceive { get; set; }
    }
}
