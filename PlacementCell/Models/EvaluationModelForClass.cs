using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementCell.Models
{
    public class EvaluationModelForClass
    {
        [Key]
        public int EvaluationID { get; set; }

        public List<PlacementCell.PlacementCellDBModel.CandidateProfileDBModel> CandidateIDList { get; set; }

        [Display(Name="Candidate Name")]
        public int CandidateID { get; set; }

        [Display(Name = "Designation Offered")]
        public string DesignationOffered { get; set; }

        [Display(Name = "Salary Agreed")]
        public long SalaryAgreed { get; set; }

        [Display(Name="Date of Joining")]
        public DateTime DOJ { get; set; }

        [Display(Name = "Location Offered")]

        public string LocationOffered { get; set; }


        [Display(Name = "Interview Result")]
        public string InterviewResult { get; set; }

        public string Comments { get; set; }

    }
}