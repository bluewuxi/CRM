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

namespace CRM.WebUI.Controllers
{
    public class ActivitiesController : BaseController
    {
        private readonly IActivityRepository _repo;

        public ActivitiesController(IActivityRepository repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            AttachUserManager(userManager);
        }

        // GET: Activities
        public IActionResult Index()
        {
            return View( _repo.GetAll());
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _repo.GetAsync(id.GetValueOrDefault());
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            ViewData["ActivityOwnerID"] = new SelectList(GetUsers(), "Id", "UserName");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityID,ActivityType,ActivityOwnerID,Subject,Content,StartTime,EndTime,AttendedAccountID,AttendedCustomerID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("ActivityID,ActivityType,ActivityOwnerID,Subject,Content,StartTime,EndTime,AttendedAccountID,AttendedCustomerID")] Activity activity)
        {
            if (id != activity.ActivityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.AttachUserContext(GetCurrentUserID());
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

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _repo.GetAsync(id.GetValueOrDefault());
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool ActivityExists(int id)
        {
            return true;
        }
    }
}
