using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PayOrder : IAggregateRoot
    {
        [Key]
        public int ID { get; set; }

        [StringLength(195)]
        public string UID { get; set; }

        [StringLength(255)]
        public string Receiver { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Total { get; set; }

        [StringLength(255)]
        public string DateOrder { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(255)]
        public string StatusPay { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        [StringLength(255)]
        public string DateConfirm { get; set; }

        [StringLength(255)]
        public string DateToShip { get; set; }

        [StringLength(255)]
        public string DateToPay { get; set; }

        [StringLength(255)]
        public string DateCancel { get; set; }
    }
}
