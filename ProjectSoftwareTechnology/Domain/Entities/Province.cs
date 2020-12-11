using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Province : IAggregateRoot
    {
        [Key]
        public int IdProvince { get; set; }

        [StringLength(255)]
        public String NameProvince { get; set; }
        public int IdReion { get; set; }
    }
}
