using CRM.Domain.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities
{
    public class VisaApplication : IEntity
    {
        public enum AppliedVisaTypeEnum: int
        {
            post=0, online, agent
        }

        public enum VisaStatusEnum : int
        {
            draft = 0, submitted, approved, declined, closed
        }

        public int VisaApplicationID { get; set; }

        public Student Student { get; set; }
        [Required]
        [Display(Name = "Student")]
        [Range(1, int.MaxValue, ErrorMessage = "Student is required")]
        public int StudentID { get; set; }

        public VisaStatusEnum Status { get; set; }

        [StringLength(20)]
        [Display(Name = "Visa Type")]
        public string VisaType { get; set; }

        [StringLength(20, MinimumLength = 8)]
        [Display(Name = "Passport No.")]
        public string PassportNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Eamil In Form")]
        public string EamilInForm { get; set; }

        [StringLength(50)]
        [Display(Name = "Physical Addr.")]
        public string PhysicalAddress { get; set; }

        [StringLength(50)]
        [Display(Name = "Postal Addr.")]
        public string PostalAddress { get; set; }

        [Display(Name = "Visa Applied Type")]
        public AppliedVisaTypeEnum VisaAppliedType { get; set; }

        public Account Institute { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Institue is required")]
        [Display(Name = "Institute")]
        public int InstituteID { get; set; }

        [StringLength(50)]
        public string Documents { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Submitted Date")]
        public DateTime? SubmittedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Received Date")]
        public DateTime? ReceivedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expired Date")]
        public DateTime? ExpiredDate { get; set; }

        [StringLength(50)]
        public string Note { get; set; }

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
