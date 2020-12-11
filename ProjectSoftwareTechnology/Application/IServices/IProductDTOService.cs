using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IProductDTOService
    {
        IEnumerable<ProductDTO> GetAllProduct();   // trả về danh sách 
        ProductDTO GetProductByID(int id);  // lấy đối tượng theo ID
        void AddProduct(ProductDTO product);
        void UpdateProduct(ProductDTO product);
        void DeleteProduct(int id);     // xóa theo ID
    }
}
