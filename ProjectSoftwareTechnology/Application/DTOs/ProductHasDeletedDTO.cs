using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public class ProductHasDeletedDTO
    {
        [Key]
        public int ID { get; set; }

        public int IDSP { get; set; }

        public int IDLSP { get; set; }

        [StringLength(255)]
        public String Name { get; set; }

        [StringLength(255)]
        public String NameBrand { get; set; }

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
    }
}
