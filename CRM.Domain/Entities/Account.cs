using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CRM.Domain.Abstract;

namespace CRM.Domain.Entities
{
    public class Account: IEntity
    {
        public enum AccountTypeEnum : int
        {
            Agent=0, Institute, Other
        }

        public int AccountID { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }


        [Display(Name = "Type")]
        public AccountTypeEnum AccountType { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Contact")]
        public string ContactName { get; set; }

        [Display(Name = "Gender")]
        public GenderEnum ContactGender { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "DOB")]
        public Nullable<DateTime> Birthdate { get; set; }

        [StringLength(50)]
        public string EMail { get; set; }

        [StringLength(30)]
        public string Mobile { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Register Date")]
        public DateTime RegisterDate { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }

        [Display(Name = "Account Owner")]
        public string AccountOwnerID { get; set; }
        public ApplicationUser AccountOwner { get; set; }

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
