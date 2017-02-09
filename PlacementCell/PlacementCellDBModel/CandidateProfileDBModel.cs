using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementCell.PlacementCellDBModel
{
    public class CandidateProfileDBModel
    {
        [Key]
        public int CanProfileID { get; set; }
        [Display(Name = "Vacancy")]
        public VacancyModelDB VacancyID { get; set; }

        [Required]
        public string CandidateName { get; set; }

        [EmailAddress]

        public string MailAddress { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DOB { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }

        [Display(Name = "10th Percentage")]
        public decimal Percentage10 { get; set; }
        [Display(Name = "12th Percentage")]
        public decimal Percentage12 { get; set; }
        [Display(Name = "Total Experience")]
        public int TotalExp { get; set; }


    }
}