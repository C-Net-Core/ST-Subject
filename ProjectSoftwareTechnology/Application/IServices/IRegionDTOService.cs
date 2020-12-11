using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IRegionDTOService
    {
        IEnumerable<RegionDTO> GetAllRegion();   // trả về danh sách 
        RegionDTO GetRegionByID(int id);  // lấy đối tượng theo ID
        void AddRegion(RegionDTO region);
        void UpdateRegion(RegionDTO region);
        void DeleteRegion(int id);     // xóa theo ID
    }
}
