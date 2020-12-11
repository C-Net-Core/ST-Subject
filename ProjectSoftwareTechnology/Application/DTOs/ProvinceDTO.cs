
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ProvinceDTO 
    {
        [Key]
        public int IdProvince { get; set; }

        [StringLength(255)]
        public String NameProvince { get; set; }
        public int IdReion { get; set; }
    }
}
