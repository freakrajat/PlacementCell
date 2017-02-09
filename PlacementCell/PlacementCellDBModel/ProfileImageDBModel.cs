using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementCell.PlacementCellDBModel
{
    public class ProfileImageDBModel
    {
        [Key]
        public int ImageID { get; set; }

        public EmployeeDBModel Emloyee { get; set; }

        public byte[] EmployeeImage { get; set; }


    }
}