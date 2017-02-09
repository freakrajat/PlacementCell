using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementCell.PlacementCellDBModel
{
    public class CandidateEvaluationDBModel
    {
        [Key]
        public int EvaluationID { get; set; }

        public CandidateProfileDBModel CandidateID { get; set; }

        public string DesignationOffered { get; set; }

        public long SalaryAgreed { get; set; }

        public DateTime DOJ { get; set; }
        public string LocationOffered { get; set; }

        public string InterviewResult { get; set; }

        public string Comments { get; set; }


    }
}