using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using CRM.Domain.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CRM.WebUI.Models;

namespace CRM.WebUI.Controllers
{
    public class HomeController : Controller
    {

        [Authorize]
        public async Task<IActionResult> Index([FromServices] EFDbContext context)
        {
            DashboardViewModel model = new DashboardViewModel();

            var openRecords = context.Activities.Where(u => u.Status == Activity.ActivityStatusEnum.OpenTask ||
                        (u.EndTime >= DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));
            var closedRecords = context.Activities.Where(u => u.Status == Activity.ActivityStatusEnum.ClosedTask ||
                        (u.EndTime < DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));
            model.openActivitiesNum = await openRecords.CountAsync();
            model.closedActivitiesNum = await closedRecords.CountAsync();
            model.studentsNum = await context.Students.CountAsync();
            model.leadsNum = await context.Leads.CountAsync();
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
