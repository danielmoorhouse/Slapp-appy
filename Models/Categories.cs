using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
   
        [Table("Category")]
        public class Cat
        {
            [Key]
            [Column("CategoryId")]
            public int CatId { get; set; }

            [Required]
            [Display(Name = "Category Name")]
            [Column("CategoryName")]
            public string CatName { get; set; }

            [Display(Name = "Category Description")]
            [Column("CategoryDescription")]
            public string CatDesc { get; set; }
      
    }
    
}