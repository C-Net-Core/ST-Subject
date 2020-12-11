
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class SaleDTO 
    {
        [Key]
        public int IDKM { get; set; }

        [StringLength(255)]
        public String phantram { get; set; }
    }
}
