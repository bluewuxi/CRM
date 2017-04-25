using CRM.Domain.Concrete;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CRM.WebUI.Models
{
    public class DashboardViewModel
    {
        public int openActivitiesNum { get; set; }
        public int closedActivitiesNum { get; set; }
        public int studentsNum { get; set; }
        public int leadsNum { get; set; }

        public DashboardViewModel()
        {
        }
    }
}
