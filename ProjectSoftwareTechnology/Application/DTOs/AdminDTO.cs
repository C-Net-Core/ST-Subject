
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AdminDTO
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
