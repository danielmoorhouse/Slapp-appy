using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
    [Table("Dev")]
    public class Develop
    {
        [Key]
        [Column("DevId")]
        public int DevId { get; set; }

        [Required]
        [Display(Name = "App Selection")]
        [Column("ApplicationId")]
        public int AppId { get; set; }

        [Required]
        [Display(Name = "Company Selection")]
        [Column("CompanyId")]
        public int CompanyId { get; set; }
    }
}