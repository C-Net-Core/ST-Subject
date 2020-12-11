
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class WardDTO 
    {
        [Key]
        public int IdWard { get; set; }

        [StringLength(255)]
        public String NameWard { get; set; }
        public int IdProvince { get; set; }
    }
}
