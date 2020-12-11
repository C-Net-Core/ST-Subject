
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs
{
    public class InfoOrderDTO
    {
        [Key]
        [Column(Order = 0)]
        public int IDP { get; set; }

        [Key]
        [Column(Order = 1)]
        public int IDSP { get; set; }

        [StringLength(255)]
        public string Amount { get; set; }

        [StringLength(255)]
        public string Size { get; set; }

        [StringLength(255)]
        public String Price { get; set; }

        [StringLength(255)]
        public string IntoMoney { get; set; }
    }
}
