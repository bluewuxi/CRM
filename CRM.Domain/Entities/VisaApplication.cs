using CRM.Domain.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities
{
    public class VisaApplication : IEntity
    {
        public enum AppliedVisaTypeEnum
        {
            post, online, agent
        }
        public int VisaApplicationID { get; set; }

        public Student Student { get; set; }
        [Required]
        public int StudentID { get; set; }

        [StringLength(20, MinimumLength = 8)]
        public string PassportNumber { get; set; }

        [StringLength(50)]
        public string EamilInForm { get; set; }

        [StringLength(50)]
        public string PhysicalAddress { get; set; }

        [StringLength(50)]
        public string PostalAddress { get; set; }

        [StringLength(50)]
        public string VisaAppliedType { get; set; }

        public Account Institute { get; set; }
        [Required]
        public int InstituteID { get; set; }

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

        public DateTime ModifiedTime { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
        public string ModifiedByID { get; set; }
        public DateTime CreatedTime { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public string CreatedByID { get; set; }
    }
}
