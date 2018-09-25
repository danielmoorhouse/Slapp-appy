using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlappMain.Models
{
  
        [Table("Pegis")]
        public class Pegi
        {
            [Key]
            [Column("PegiId")]
            public int PegiId { get; set; }

            [Required]
            [Display(Name = "Pegi Rating")]
            [Column("PegiRating")]
            public string PegRate { get; set; }

            [Required]
            [Display(Name = "Pegi Description")]
            [Column("PegiDesc")]
            public string PegDesc { get; set; }


        [Display(Name = "Pegi Image")]
            [Column("PegiImageUrl")]
            public string PegImg { get; set; }

    }
    }
