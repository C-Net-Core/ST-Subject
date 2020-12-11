using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public class PayOrderCancelDTO
    {
        [Key]
        public int ID { get; set; }

        public int IDP { get; set; }

        [StringLength(255)]
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
        public string DateCancel { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(255)]
        public string StatusPay { get; set; }

        [StringLength(255)]
        public string Note { get; set; }
    }
}
