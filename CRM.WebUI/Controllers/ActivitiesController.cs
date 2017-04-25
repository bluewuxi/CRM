using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.WebUI.Models;
using CRM.Domain.Entities;
using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using CRM.Domain.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CRM.WebUI.Controllers
{
    [Authorize]
    public class ActivitiesController : BaseController
    {
        private readonly IActivityRepository _repo;

        public ActivitiesController(IActivityRepository repo, UserManager<ApplicationUser> userManager):base(userManager)
        {
            _repo = repo;
        }

        // GET: Activities
        public IActionResult Index()
        {
            //We use RESPful WebApi do list , leave here empty.
            return View();
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BindUserContext(_repo);
            var activity = await _repo.GetAsync(id.GetValueOrDefault());
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // GET: Activities/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ActivityOwnerID"] = new SelectList(GetUsers(), "Id", "UserName");
            return View(new Activity() { ActivityOwnerID = await GetUserContextAsync(),
                Status =Activity.ActivityStatusEnum.Event,
                ActivityType = Activity.ActivityTypeEnum.OutboundCall});
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityID,ActivityType,Status,ActivityOwnerID,Subject,Content,StartTime,EndTime,AttendedAccountID,AttendedCustomerID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                BindUserContext(_repo);
                await _repo.AddAsync(activity);
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BindUserContext(_repo);
            var activity = await _repo.GetAsync(id.GetValueOrDefault());
            if (activity == null)
            {
                return NotFound();
            }
            ViewData["ActivityOwnerID"] = new SelectList(GetUsers(), "Id", "UserName", activity.ActivityOwnerID??"");
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityID,Status,ActivityType,ActivityOwnerID,Subject,Content,StartTime,EndTime,AttendedAccountID,AttendedCustomerID")] Activity activity)
        {
            if (id != activity.ActivityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    BindUserContext(_repo);
                    await _repo.UpdateAsync(activity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.ActivityID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ActivityOwnerID"] = new SelectList(GetUsers(), "Id", "UserName", activity.ActivityOwnerID??"");
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BindUserContext(_repo);
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool ActivityExists(int id)
        {
            return true;
        }
    }
}
