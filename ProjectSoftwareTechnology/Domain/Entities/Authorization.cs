using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Authorization : IAggregateRoot
    {
        public int IDAdmin { get; set; }

        public int IDCN { get; set; }
    }
}
