using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IInfoOrderDTOService
    {
        IEnumerable<InfoOrderDTO> GetAllInfoOrder();   // trả về danh sách 
        InfoOrderDTO GetInfoOrderByID(int id);  // lấy đối tượng theo ID
        void AddInfoOrder(InfoOrderDTO infoOrder);
        void UpdateInfoOrder(InfoOrderDTO infoOrder);
        void DeleteInfoOrder(int id);     // xóa theo ID
    }
}
