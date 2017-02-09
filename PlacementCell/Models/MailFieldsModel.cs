using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementCell.Models
{
    public class MailFieldsModel
    {
        [Display(Name="Recipient Mail ID")]
        public string To { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public List<string> Emails { get; set; }
    }
}