using Application.DTOs;
using WebApp_Cnpm.Models.Helper;

namespace WebApp_Cnpm.ViewModels
{
    public class AddProductsModel
    {
        public PaginatedList<ProductDTO> Products { get; set; }
        public AddProductKeys Keys { get; set; }
        public EditProductKeys EditKeys { get; set; }
    }
}
