using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Product : IAggregateRoot
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
