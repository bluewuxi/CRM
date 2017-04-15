using CRM.Domain.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Domain.Entities
{
    public class Enrollment : IEntity
    {
        public enum EnrollmentStatusEnum : int
        {
            Actived=0, Closed
        }

        public int EnrollmentID { get; set; }
        public Student Student { get; set; }
        public int StudentID { get; set; }

        public Account Institute { get; set; }
        [Required]
        public int InstituteID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Due Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public Decimal Tuition { get; set; }

        public Account EnrollmentAgent { get; set; }
        public int? EnrollmentAgentID { get; set; }
        public EnrollmentStatusEnum Status {get; set;}
        public string Note { get; set; }

        public DateTime ModifiedTime { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
        public string ModifiedByID { get; set; }
        public DateTime CreatedTime { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public string CreatedByID { get; set; }
    }
}
