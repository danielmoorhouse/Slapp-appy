using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
    [Table("Platform")]
    public class Platform
    {
        [Key]
        [Column("PlatformId")]
        public int PlatId { get; set; }

        [Required]
        [Display(Name = "Platform")]
        [Column("PlatformName")]
        public string PlatName { get; set; }
    }
}