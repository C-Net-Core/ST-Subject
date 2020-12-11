using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IProductHasDeletedDTOService
    {
        IEnumerable<ProductHasDeletedDTO> GetAll();   // trả về danh sách 
        ProductHasDeletedDTO GetByID(int id);  // lấy đối tượng theo ID
        void Add(ProductHasDeletedDTO productDTO);
        void Delete(int id);     // xóa theo ID
    }
}
