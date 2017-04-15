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

        public int ActivityID { get; set; }

        public ActivityTypeEnum  ActivityType { get; set; }

        public ApplicationUser ActivityOwner { get; set; }
        public string ActivityOwnerID { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Subject { get; set; }

        public string Content { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }


        public Account AttendedAccount { get; set; }
        public int? AttendedAccountID { get; set; }

        public Customer AttendedCustomer { get; set; }
        public int? AttendedCustomerID { get; set; }

        public DateTime ModifiedTime { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
        public string ModifiedByID { get; set; }
        public DateTime CreatedTime { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public string CreatedByID { get; set; }

    }
}
