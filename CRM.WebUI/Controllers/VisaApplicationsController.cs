using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Domain.Concrete;
using CRM.Domain.Entities;
using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity;
using CRM.WebUI.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace CRM.WebUI.Controllers
{
    public class VisaApplicationsController : BaseController
    {
        private readonly EFDbContext _context;
        private readonly IVisaApplicationRepository _repo;

        public VisaApplicationsController(EFDbContext context, IVisaApplicationRepository repo, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _repo = repo;
            _context = context;
        }

        // GET: VisaApplications
        public IActionResult Index()
        {
            //We use RESPful WebApi do list accounts, leave here empty.
            var querySetting = HttpContext.Session.Get<QuerySettingViewModel>("VisaApplicationsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            return View(querySetting);
        }

        // POST: Set VisaApplications Filter
        [HttpPost]
        public void SetQuery(string search = "", string sort = "", long offset = 0)
        {
            QuerySettingViewModel querySetting = HttpContext.Session.Get<QuerySettingViewModel>("VisaApplicationsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            else
                querySetting.search.Clear();

            if (search != null && search != "")
                querySetting.search = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
            HttpContext.Session.Set<QuerySettingViewModel>("VisaApplicationsList", querySetting);
            string a = HttpContext.Session.GetString("VisaApplicationsList");
            Response.Redirect("/VisaApplications/Index");
        }

        // GET: VisaApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BindUserContext(_repo);
            var visaApplication = await _repo.GetAsync(id.GetValueOrDefault());
            if (visaApplication == null)
            {
                return NotFound();
            }

            return View(visaApplication);
        }

        // GET: VisaApplications/Create
        public IActionResult Create()
        {
            return View(new VisaApplication() {VisaAppliedType="student"});
        }

        // POST: VisaApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisaApplicationID,InstituteID,StudentID,PassportNumber,EamilInForm,PhysicalAddress,PostalAddress,VisaAppliedType,Documents,SubmittedDate,ReceivedDate,ExpiredDate,Note,ModifiedTime,CreatedTime")] VisaApplication visaApplication)
        {
            if (ModelState.IsValid)
            {
                BindUserContext(_repo);
                await _repo.AddAsync(visaApplication);
                return RedirectToAction("Index");
            }
            return View(visaApplication);
        }

        // GET: VisaApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BindUserContext(_repo);
            var visaApplication = await _repo.GetAsync(id.GetValueOrDefault());
            if (visaApplication == null)
            {
                return NotFound();
            }
            return View(visaApplication);
        }

        // POST: VisaApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisaApplicationID,InstituteID,StudentID,PassportNumber,EamilInForm,PhysicalAddress,PostalAddress,VisaAppliedType,Documents,SubmittedDate,ReceivedDate,ExpiredDate,Note,ModifiedTime,CreatedTime")] VisaApplication visaApplication)
        {
            if (id != visaApplication.VisaApplicationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    BindUserContext(_repo);
                    await _repo.UpdateAsync(visaApplication);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisaApplicationExists(visaApplication.VisaApplicationID))
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
            return View(visaApplication);
        }

        // POST: VisaApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BindUserContext(_repo);
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool VisaApplicationExists(int id)
        {
            return _repo.Get(id)==null?false:true;
        }
    }
}
