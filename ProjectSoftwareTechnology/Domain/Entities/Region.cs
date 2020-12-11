using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Region : IAggregateRoot
    {
        [Key]
        public int IdRegion { get; set; }

        [StringLength(255)]
        public String NameArea { get; set; }

        public int FeeShip { get; set; }
    }
}
