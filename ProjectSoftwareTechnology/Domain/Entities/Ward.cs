using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ward : IAggregateRoot
    {
        [Key]
        public int IdWard { get; set; }

        [StringLength(255)]
        public String NameWard { get; set; }
        public int IdProvince { get; set; }
    }
}
