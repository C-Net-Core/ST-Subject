using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin : IAggregateRoot
    {
        [Key]
        public int ID { get; set; }

        [StringLength(255)]
        public String UID { get; set; }

        [StringLength(255)]
        public String PW { get; set; }

        [StringLength(255)]
        public String Permission { get; set; }

        [StringLength(255)]
        public String DateActive { get; set; }

        [StringLength(255)]
        public String Status { get; set; }
    }
}
