using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IProductTypeDTOService
    {
        IEnumerable<ProductTypeDTO> GetAllProductType();   // trả về danh sách 
        ProductTypeDTO GetProductTypeByID(int id);  // lấy đối tượng theo ID
        void AddProductType(ProductTypeDTO productType);
        void UpdateProductType(ProductTypeDTO productType);
        void DeleteProductType(int id);     // xóa theo ID
    }
}
