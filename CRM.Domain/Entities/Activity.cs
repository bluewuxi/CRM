using CRM.Domain.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities
{

    public class Activity: IEntity
    {
        public enum ActivityTypeEnum: int
        {
            OutboundCall=1, InboundCall, Message, Meeting, Email, Visit, Other
        }

        public enum ActivityStatusEnum : int
        {
            Event = 1, OpenTask, ClosedTask
        }

        public int ActivityID { get; set; }

        public ActivityStatusEnum Status { get; set; }

        [Display(Name = "Activity Type")]
        public ActivityTypeEnum  ActivityType { get; set; }

        public ApplicationUser ActivityOwner { get; set; }
        [Display(Name = "Activity Owner")]
        public string ActivityOwnerID { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Subject { get; set; }

        public string Content { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time")]
        public DateTime? StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        public Account AttendedAccount { get; set; }
        [Display(Name = "Attended Account")]
        public int? AttendedAccountID { get; set; }

        public Customer AttendedCustomer { get; set; }
        [Display(Name = "Attended Customer")]
        public int? AttendedCustomerID { get; set; }

        [Display(Name = "Modified Time")]
        public DateTime ModifiedTime { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
        public string ModifiedByID { get; set; }

        [Display(Name = "Created Time")]
        public DateTime CreatedTime { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public string CreatedByID { get; set; }

    }
}
