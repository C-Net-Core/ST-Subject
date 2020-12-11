using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sale : IAggregateRoot
    {
        [Key]
        public int IDKM { get; set; }

        [StringLength(255)]
        public String phantram { get; set; }
    }
}
