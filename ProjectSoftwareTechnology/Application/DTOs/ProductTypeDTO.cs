
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ProductTypeDTO 
    {
        [Key]
        public int IDLSP { get; set; }

        [StringLength(255)]
        public String Name { get; set; }

        [StringLength(255)]
        public String Filter { get; set; }
    }
}
