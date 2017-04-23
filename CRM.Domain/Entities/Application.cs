using CRM.Domain.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Domain.Entities
{
    public class Application : IEntity
    {
        public enum ApplicationStatusEnum : int
        {
            Draft = 0, Applied, Reserved, Accepted, AcceptedWithCondition, Closed
        }

        public int ApplicationID { get; set; }
        public Student Student { get; set; }
        [Required]
        [Display(Name = "Student")]
        public int StudentID { get; set; }

        public Account Institute { get; set; }
        [Required]
        [Display(Name = "Institute")]
        public int InstituteID { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public Decimal Tuition { get; set; }

        public Account ApplicationAgent { get; set; }
        [Display(Name = "Agent")]
        public int? ApplicationAgentID { get; set; }
        [Required]
        public ApplicationStatusEnum Status { get; set; }
        public string Note { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Submitted Date")]
        public DateTime SubmittedDate { get; set; }

        public DateTime ModifiedTime { get; set; }
        [Display(Name = "Modified By")]
        public ApplicationUser ModifiedBy { get; set; }
        [Display(Name = "Created By")]
        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }

        public string ModifiedByID { get; set; }
        public string CreatedByID { get; set; }
    }
}
