using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities
{
    public class Lead : Customer
    {
        [StringLength(50)]
        public string Source { get; set; }
    }
}

