using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacementCell.Models
{
    public class UserProfileModel
    {
        public IEnumerable<LoginModelForClass> LoginDetailsOfUser { get; set; }

        public IEnumerable<EmployeeModelForClass> DetailsOfUser { get; set; }
    }
}