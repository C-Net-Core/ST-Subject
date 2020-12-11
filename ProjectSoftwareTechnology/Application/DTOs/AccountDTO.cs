
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AccountDTO
    {
        
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public String UID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [StringLength(30)]
        public String PW { get; set; }

        [StringLength(255)]
        public String Status { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public String Address { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public String Email { get; set; }

        [StringLength(255)]
        public String DateActive { get; set; }

    }
}
