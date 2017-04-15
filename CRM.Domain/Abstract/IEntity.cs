using CRM.Domain.Entities;
using System;

namespace CRM.Domain.Abstract
{
    public interface IEntity
    {
        DateTime ModifiedTime { get; set; }
        ApplicationUser ModifiedBy { get; set; }
        string ModifiedByID { get; set; }
        DateTime CreatedTime { get; set; }
        ApplicationUser CreatedBy { get; set; }
        string CreatedByID { get; set; }
    }
}
