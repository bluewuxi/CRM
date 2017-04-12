using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string PreferName { get; set; }

        public GenderEnum Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string Nationality { get; set; }

        [StringLength(20, MinimumLength = 8)]
        public string PassportNumber { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string ContactName { get; set; }

        [StringLength(50)]
        public string EMail { get; set; }

        [StringLength(30)]
        public string Mobile { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }

        [StringLength(200)]
        public string AcademicBackground { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public string Note { get; set; }

        public Account Agent { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<VisaApplication> VisaApplications { get; set; }
        public ICollection<Activity> Activities { get; set; }

        [Display(Name = "Student Owner")]
        public ApplicationUser StudentOwner { get; set; }

        [Display(Name = "Modified By")]
        public ApplicationUser ModifiedBy { get; set; }
    }
}
