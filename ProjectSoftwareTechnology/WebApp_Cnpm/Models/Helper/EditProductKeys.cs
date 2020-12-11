using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Models.Helper
{
    public class EditProductKeys
    {
        public string productIDEdit { get; set; }
        public string productTypeIDEdit { get; set; }
        public string nameEdit { get; set; }
        public string costEdit { get; set; }
        public string decriptionEdit { get; set; }
        public IFormFile imageEdit { get; set; }
        public int amountEdit { get; set; }
        public string sizeEdit { get; set; }
        public string saleIDEdit { get; set; }
    }
}
