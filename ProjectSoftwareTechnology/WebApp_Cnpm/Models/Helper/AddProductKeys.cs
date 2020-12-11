
using Microsoft.AspNetCore.Http;

namespace WebApp_Cnpm.Models.Helper
{
    public class AddProductKeys
    {
        public string productTypeID { get; set; }
        public string name { get; set; }
        public string cost { get; set; }
        public string decription { get; set; }
        public IFormFile image { get; set; }
        public int amount { get; set; }
        public string size { get; set; }
        public string saleID { get; set; }

    }
}
