using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;
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
        [Display(Name = "Passport No.")]
        public string PassportNumber { get; set; }

        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Client No.")]
        public string ClientNumber { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Contact")]
        public string ContactName { get; set; }

        public Account Agent { get; set; }
        [Display(Name = "Agent")]
        public int? AgentID { get; set; }

        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; set; }
        [JsonIgnore]
        public ICollection<VisaApplication> VisaApplications { get; set; }
        [JsonIgnore]
        public ICollection<Application> Applications { get; set; }
    }
}
