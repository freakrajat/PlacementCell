using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PlacementCell.PlacementCellDBModel;

namespace PlacementCell.Models
{
    public class EmployeeModelForClass
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]

        [Display(Name="Employee Name")]
        public string EmployeeName { get; set; }

        public LoginDBModel UserName { get; set; }

        public string  Location { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }
        [Required]
        [Display(Name = "Date of Joining")]
        public DateTime? DOJ { get; set; }
        [Required]
        public int CTC { get; set; }

        public string Designation { get; set; }
        [Required]
        public string Email { get; set; }

        public PlacementCell.PlacementCellDBModel.ProfileImageDBModel Image { get; set; }

        public System.Drawing.Image ImageFormat  { get; set; }

        //Property for body tg element in offer letter
               public string  msg { get; set; }
    }
}