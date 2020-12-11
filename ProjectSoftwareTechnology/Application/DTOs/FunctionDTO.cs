using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public class FunctionDTO
    {
        [Key]
        public int IDCN { get; set; }

        [StringLength(255)]
        public String Name { get; set; }

        [StringLength(255)]
        public String NameMethod { get; set; }
    }
}
