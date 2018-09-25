using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
    [Table("ApplicationPlatform")]
    public class AppPlats
    {
        [Key]
        [Column("ApplicationPlatformId")]
        public int AppPlatId { get; set; }

        [Required]
        [Display(Name = "Application")]
        [Column("ApplicationId")]
        public int AppId { get; set; }

        [Required]
        [Display(Name = "Platform")]
        [Column("PlatformId")]
        public int PlatId { get; set; }
    }
}