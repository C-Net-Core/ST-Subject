using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IPayOrderCancelDTOService
    {
        IEnumerable<PayOrderCancelDTO> GetAllPayOrderCancel();   // trả về danh sách 
        PayOrderCancelDTO GetPayOrderCancelByID(int id);  // lấy đối tượng theo ID
        void AddPayOrderCancel(PayOrderCancelDTO payOrderCancel);
        void UpdatePayOrderCancel(PayOrderCancelDTO payOrderCancel);
        void DeletePayOrderCancel(int id);     // xóa theo ID

        int countStatus(string status);
        int countAllOrder();
    }
}
