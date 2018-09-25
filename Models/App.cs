using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlappMain.Models
{

    [Table("Application")]
    public class App
    {
       

        [Key]
        [Column("ApplicationId")]
        public int AppId { get; set; }

        [Column("ApplicationName")]
        [Display(Name = "App Name")]
        public string AppName { get; set; }

        [Column("ApplicationDescription")]
        [Display(Name = "Description")]
        public string AppDesc { get; set; }


        [Column("ReleaseDate")]
        [Display(Name = "Released")]
        [DataType(DataType.Date)]
        public DateTime? AppDate { get; set; }

        [Column("ApplicationImageUrl1")]
        [Display(Name = "Image 1")]
        public string AppImg1 { get; set; }



        [Column("ApplicationImageUrl2")]
        [Display(Name = "Image 2")]
        public string AppImg2 { get; set; }

        [Column("ApplicationPrice")]
        [Display(Name = "Price")]
        public decimal AppPrice { get; set; }


        [Column("ApplicationTileUrl")]
        [Display(Name = "App Tile")]
        public string AppTile { get; set; }

        //trimmed copy of app description
        public string AppDescTrimmed
        {
            get
            {
                //get only; updates are to AppDesc
                if ((AppDesc.Length) > 100)

                    //get a substring of the first 100 characters followed by 3 elipses
                    return AppDesc.Substring(0, 100) + "...";
                else
                    //otherwise return the full description
                    return AppDesc;
            }
        }

    }

}