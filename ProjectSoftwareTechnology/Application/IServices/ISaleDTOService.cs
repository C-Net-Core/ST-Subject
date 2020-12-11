using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface ISaleDTOService
    {
        IEnumerable<SaleDTO> GetAllSale();   // trả về danh sách 
        SaleDTO GetSaleByID(int id);  // lấy đối tượng theo ID
        void AddSale(SaleDTO sale);
        void UpdateSale(SaleDTO sale);
        void DeleteSale(int id);     // xóa theo ID
    }
}
