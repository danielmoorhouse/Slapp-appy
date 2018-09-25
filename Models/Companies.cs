using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
    [Table("Company")]
    public class Companies
    {
        [Key]
        [Column("CompanyId")]
        public int CompId { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        [Column("CompanyName")]
        public string CompName { get; set; }

       [Required]
       [Display(Name = "Company Logo")]
       [Column("CompanyLogo")]
       public string CompLogo { get; set; }

        [Required]
        [Display(Name = "Company Contact")]
        [Column("CompanyContact")]
        public string CompCont { get; set; }

    }
}