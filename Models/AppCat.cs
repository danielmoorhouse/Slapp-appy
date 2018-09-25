using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
    [Table("ApplicationCategory")]
    public class AppCat
    {


        [Key]
        [Column("ApplicationCategoryId")]
        public int AppCatId { get; set; }

        [Column("ApplicationId")]
        [Display(Name = "App Selection")]
        public int AppId { get; set; }

        [Column("CategoryId")]
        [Display(Name = "Category Selection")]
        public int CatsId { get; set; }
       
    }
}