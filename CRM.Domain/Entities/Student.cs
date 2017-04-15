using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities
{
    public class Student: Customer
    {
        [StringLength(20)]
        public string Rating { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string Nationality { get; set; }

        [StringLength(20, MinimumLength = 5)]
        public string PassportNumber { get; set; }

        [StringLength(20, MinimumLength = 6)]
        public string ClientNumber { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string ContactName { get; set; }

        public Account Agent { get; set; }
        public int? AgentID { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<VisaApplication> VisaApplications { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
