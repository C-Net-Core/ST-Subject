using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IProvinceDTOService
    {
        IEnumerable<ProvinceDTO> GetAllProvince();   // trả về danh sách 
        ProvinceDTO GetProvinceByID(int id);  // lấy đối tượng theo ID
        void AddProvince(ProvinceDTO province);
        void UpdateProvince(ProvinceDTO province);
        void DeleteProvince(int id);     // xóa theo ID
    }
}
