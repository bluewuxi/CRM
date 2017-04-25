using CRM.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CRM.WebUI.ViewComponents
{
    public class RecordInfoViewComponent : ViewComponent
    {
        public RecordInfoViewComponent()
        {
        }

        public IViewComponentResult Invoke(ApplicationUser createdBy, ApplicationUser modifiedBy, DateTime createdTime, DateTime modifiedTime)
        {
            string sCreatedTime ="", sModifiedTime="";

            sCreatedTime=createdTime.ToString("yyyy-MM-dd HH:mm:ss");
            sModifiedTime = modifiedTime.ToString("yyyy-MM-dd HH:mm:ss");

            return View(new string[] { createdBy == null ? "" : createdBy.UserName,sCreatedTime,
                modifiedBy == null ? "" : modifiedBy.UserName, sModifiedTime } );
        }

    }

}
