using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace CRM.WebUI.ViewComponents
{
    public class ActivityInfoViewComponent : ViewComponent
    {
        private IActivityRepository _repo;

        public ActivityInfoViewComponent([FromServices] IActivityRepository repo)
        {
            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync(string queryCode, int? referenceId)
        {
            int iOpen = 0, iClosed = 0;
            queryCode = queryCode.ToLower().Trim();
            if (queryCode == "customer" && referenceId != null)
            {
                iOpen = await _repo.GetAll().Where(u => u.AttendedCustomerID == referenceId && (u.Status == Activity.ActivityStatusEnum.OpenTask ||
                         (u.EndTime >= DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event))).CountAsync();
                iClosed = await _repo.GetAll().Where(u => u.AttendedCustomerID == referenceId && (u.Status == Activity.ActivityStatusEnum.ClosedTask ||
                        (u.EndTime < DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event))).CountAsync();
            }
            if (queryCode == "account" && referenceId != null)
            {
                iOpen = await _repo.GetAll().Where(u => u.AttendedAccountID == referenceId && (u.Status == Activity.ActivityStatusEnum.OpenTask ||
                         (u.EndTime >= DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event))).CountAsync();
                iClosed = await _repo.GetAll().Where(u => u.AttendedAccountID == referenceId && (u.Status == Activity.ActivityStatusEnum.ClosedTask ||
                        (u.EndTime < DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event))).CountAsync();
            }

            return View(new string[] {iOpen.ToString(), iClosed.ToString() });
        }

    }

}
