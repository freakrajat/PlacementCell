using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlacementCell.PlacementCellDBModel;
using System.Linq;
using System.Web;

namespace PlacementCell.Models
{
    public class PlacementDBTestForClass
    {
        [Key]
        public int TestID { get; set; }

        [Required]
        [Display(Name = "Test Invigilator")]


        public int TestAdministrator { get; set; }

        [Required]
        [Display(Name= "Select Candidate")]


        public int SelectCandidate { get; set; }

        [Required]
        public int Vacancy { get; set; }
        [Display(Name = "Written Round")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TestDate { get; set; }

        [Display(Name = "Technical Interview")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TechnicalInterviewDate { get; set; }

        [Display(Name = "HR Interview")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? HrInterviewDate { get; set; }

        public List<EmployeeDBModel> employeeList { get; set; }

        public List<CandidateProfileDBModel> candidateList { get; set; }

        public List<VacancyModelDB> vacancyList { get; set; }

        public string CandidateName { get; set; }

        public string EmployeeName { get; set; }

        public string VacancyTitle { get; set; }
    }
}