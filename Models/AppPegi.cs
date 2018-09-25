using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
    [Table("AppPegi")]
    public class AppPegi
    {
        [Key]
        [Column("AppPegiId")]
        public int AppPegiId { get; set; }

      
        [Required]
        [Display(Name = "App Selection")]
        [Column("ApplicationId")]
        public int AppId { get; set; }

        [Required]
        [Display(Name = "Pegi Selection")]
        [Column("PegiId")]
        public int PegiId { get; set; }
    }
}