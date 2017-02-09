using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementCell.Models
{
    public class VacancyModelForClass
    {
        [Key]
        public int VacancyID { get; set; }

        [Required]
        [Display(Name="Title")]
        public string VacancyTitle { get; set; }

        [Display(Name="Num of Positions")]
        public int NumPosition { get; set; }

        [MaxLength(500)]
         [Display(Name = "Skills Required")]
        public string Skills { get; set; }
         [Display(Name="Min Experience Requried")]
         public int ExpRequired { get; set; }

         public string Location { get; set; }

         public string Domain { get; set; }
         [Display(Name="Posting Date")]
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
         public DateTime? PostingDate { get; set; }

         public string DatePosted { get; set; }
    }
}