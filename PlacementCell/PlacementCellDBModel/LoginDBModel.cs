using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PlacementCell.PlacementCellDBModel
{
    public class LoginDBModel
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]

        public string Role { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}