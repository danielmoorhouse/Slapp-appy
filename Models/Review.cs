using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [Required]
        [Column("ReviewId")]
        public int RevId { get; set; }

        [Required]
        [Column("ApplicationId")]
        [Display(Name = "App")]
        public int AppId { get; set; }

        [Required]
        [Column("Name")]
        [Display(Name = "Username")]
        public string RevUname { get; set; }

        [Required]
        [Column("ReviewExplained")]
        [Display(Name = "Review Content")]
        [DataType(DataType.MultilineText)]
        public string ReviewContent { get; set; }

        [Required]
        [Column("ReviewRating")]
        [Display(Name = "Rating")]
        public int ReviewRating { get; set; }

        public string ReviewContentTrimmed
        {
            get
            {
                //if longer than 100 characters, trim and add 3 dots
                if ((ReviewContent.Length) > 100)
                    return ReviewContent.Substring(0, 100) + "......Read More";
                else
                    return ReviewContent;

            }
        }
    }
}