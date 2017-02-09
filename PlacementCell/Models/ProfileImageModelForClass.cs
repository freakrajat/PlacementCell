using PlacementCell.PlacementCellDBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementCell.Models
{
    public class ProfileImageModelForClass
    {
        [Key]
        public int ImageID { get; set; }

        public EmployeeDBModel Emloyee { get; set; }

        [Display(Name="Image")]
        public byte[] EmployeeImage { get; set; }

    }
}