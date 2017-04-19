using CRM.Domain.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities
{
    public class Customer : IEntity
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string PreferName { get; set; }

        public GenderEnum Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [StringLength(200)]
        public string AcademicBackground { get; set; }

        [StringLength(50)]
        public string EMail { get; set; }

        [StringLength(30)]
        public string Mobile { get; set; }


        [StringLength(200)]
        public string Address { get; set; }

        public string Note { get; set; }

        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; }

        [Display(Name = "Customer Owner")]
        public ApplicationUser CustomerOwner { get; set; }
        public string CustomerOwnerID { get; set; }

        public DateTime ModifiedTime { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
        public string ModifiedByID { get; set; }
        public DateTime CreatedTime { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public string CreatedByID { get; set; }
    }
}
