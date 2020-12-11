using Application.DTOs;
using System.Collections.Generic;
using WebApp_Cnpm.Models.Helper;

namespace WebApp_Cnpm.ViewModels
{
    public class ProductTypeViewModel
    {
        public PaginatedList<ProductTypeDTO> ProductTypes{ get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }

        public AddBrandKeys BrandKeys { get; set; }
        public AddBrandKeys EditBrandKeys { get; set; }
    }
}
