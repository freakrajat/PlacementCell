using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementCell.PlacementCellDBModel
{
    public class PlacementDBTest
    {
        [Key]
        public int TestID { get; set; }

        [Required]
        [Display(Name = "Test Invigilator")]


        public EmployeeDBModel TestAdministrator { get; set; }

        [Required]
        [Display(Name= "Select Candidate")]
        public CandidateProfileDBModel SelectCandidate { get; set; }

        [Required]
       public VacancyModelDB Vacancy { get; set; }
        [Display(Name = "Written Round")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TestDate { get; set; }

        [Display(Name = "Technical Interview")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TechnicalInterviewDate { get; set; }

        [Display(Name = "HR Interview")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? HrInterviewDate { get; set; }

    }
}