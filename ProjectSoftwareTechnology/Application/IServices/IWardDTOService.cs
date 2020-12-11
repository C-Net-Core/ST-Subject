using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IWardDTOService
    {
        IEnumerable<WardDTO> GetAllWard();   // trả về danh sách 
        WardDTO GetWardByID(int id);  // lấy đối tượng theo ID
        void AddWard(WardDTO ward);
        void UpdateWard(WardDTO ward);
        void DeleteWard(int id);     // xóa theo ID
    }
}
