using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.ViewModels.AdminViewModels
{
    public class ShippingFeeViewModel
    {
        public List<RegionDTO> Regions { get; set; }
        public List<ProvinceDTO> Provinces { get; set; }
        public List<WardDTO> Wards { get; set; }
        public string dsMienBac { get; set; }
        public string dsMienTrung { get; set; }
        public String dsMienNam { get; set; }
        public int demTPMienBac { get; set; }
        public int demQuanMienBac { get; set; }
        public int demTPMienTrung { get; set; }
        public int demQuanMienTrung { get; set; }
        public int demTPMienNam { get; set; }
        public int demQuanMienNam { get; set; }
        public string idRegion { get; set; }
        public string feeship { get; set; }
    }
}
