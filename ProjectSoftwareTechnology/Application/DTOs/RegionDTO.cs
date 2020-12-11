
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RegionDTO 
    {
        [Key]
        public int IdRegion { get; set; }

        [StringLength(255)]
        public String NameArea { get; set; }

        public int FeeShip { get; set; }
    }
}
