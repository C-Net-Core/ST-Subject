using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductType : IAggregateRoot
    {
        [Key]
        public int IDLSP { get; set; }

        [StringLength(255)]
        public String Name { get; set; }

        [StringLength(255)]
        public String Filter { get; set; }
    }
}
