using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRM.Domain.Entities
{

    public class Activity
    {
        public enum ActivityTypeEnum
        {
            OutboundCall, InboundCall, Message, Meeting, Email, Visit, Other
        }

        public int ActivityID { get; set; }

        ActivityTypeEnum  ActivityType { get; set; }
        ApplicationUser ActivityOwner { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Subject { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ActivityTime { get; set; }

        [Required]
        public string Content { get; set; }

        public Account AttendAccount { get; set; }

        public Student AttendStudent { get; set; }
    }
}
