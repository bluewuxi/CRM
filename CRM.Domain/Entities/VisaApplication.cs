using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Domain.Entities
{
    public class VisaApplication
    {
        public enum AppliedVisaTypeEnum
        {
            post, online, agent
        }
        public int VisaApplicationID { get; set; }

        public Student Student { get; set; }

        [StringLength(20, MinimumLength = 8)]
        public string PassportNumber { get; set; }

        [StringLength(20, MinimumLength = 6)]
        public string ClientNumber { get; set; }

        [StringLength(50)]
        public string EamilInForm { get; set; }

        [StringLength(50)]
        public string PhysicalAddress { get; set; }

        [StringLength(50)]
        public string PostalAddress { get; set; }

        [StringLength(50)]
        public string VisaAppliedType { get; set; }

        public Account Institute { get; set; }

        [StringLength(50)]
        public string Documents { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmittedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceivedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiredDate { get; set; }

        [StringLength(50)]
        public string Note { get; set; }
    }
}
