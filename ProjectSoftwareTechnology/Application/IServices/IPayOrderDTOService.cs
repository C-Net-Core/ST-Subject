using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IPayOrderDTOService
    {
        IEnumerable<PayOrderDTO> GetAllPayOrder();   // trả về danh sách 
        PayOrderDTO GetPayOrderByID(int id);  // lấy đối tượng theo ID
        void AddPayOrder(PayOrderDTO payOrder);
        void UpdatePayOrder(PayOrderDTO payOrder);
        void DeletePayOrder(int id);     // xóa theo ID

        int countStatus(string status);
        int countAllOrder();
    }
}
