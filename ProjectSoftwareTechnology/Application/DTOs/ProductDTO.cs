
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ProductDTO
    {
        [Key]
        public int IDSP { get; set; }

        [StringLength(255)]
        public String Name { get; set; }

        [StringLength(255)]
        public String Cost { get; set; }

        [StringLength(255)]
        public String Descrip { get; set; }

        [StringLength(255)]
        public String Image { get; set; }

        [StringLength(255)]
        public String Amount { get; set; }

        public int IDKM { get; set; }

        [StringLength(255)]
        public String Size { get; set; }

        public int IDLSP { get; set; }
    }
}
