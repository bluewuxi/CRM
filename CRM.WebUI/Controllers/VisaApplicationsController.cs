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
        public async Task<IActionResult> Index()
        {
            var eFDbContext = _context.VisaApplications.Include(v => v.Student);
            return View(await eFDbContext.ToListAsync());
        }

        // GET: VisaApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaApplication = await _context.VisaApplications
                .Include(v => v.Student)
                .SingleOrDefaultAsync(m => m.VisaApplicationID == id);
            if (visaApplication == null)
            {
                return NotFound();
            }

            return View(visaApplication);
        }

        // GET: VisaApplications/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name");
            return View();
        }

        // POST: VisaApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisaApplicationID,StudentID,PassportNumber,EamilInForm,PhysicalAddress,PostalAddress,VisaAppliedType,Documents,SubmittedDate,ReceivedDate,ExpiredDate,Note,ModifiedTime,CreatedTime")] VisaApplication visaApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visaApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name", visaApplication.StudentID);
            return View(visaApplication);
        }

        // GET: VisaApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaApplication = await _context.VisaApplications.SingleOrDefaultAsync(m => m.VisaApplicationID == id);
            if (visaApplication == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name", visaApplication.StudentID);
            return View(visaApplication);
        }

        // POST: VisaApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisaApplicationID,StudentID,PassportNumber,EamilInForm,PhysicalAddress,PostalAddress,VisaAppliedType,Documents,SubmittedDate,ReceivedDate,ExpiredDate,Note,ModifiedTime,CreatedTime")] VisaApplication visaApplication)
        {
            if (id != visaApplication.VisaApplicationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visaApplication);
                    await _context.SaveChangesAsync();
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
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name", visaApplication.StudentID);
            return View(visaApplication);
        }

        // GET: VisaApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visaApplication = await _context.VisaApplications
                .Include(v => v.Student)
                .SingleOrDefaultAsync(m => m.VisaApplicationID == id);
            if (visaApplication == null)
            {
                return NotFound();
            }

            return View(visaApplication);
        }

        // POST: VisaApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visaApplication = await _context.VisaApplications.SingleOrDefaultAsync(m => m.VisaApplicationID == id);
            _context.VisaApplications.Remove(visaApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VisaApplicationExists(int id)
        {
            return _context.VisaApplications.Any(e => e.VisaApplicationID == id);
        }
    }
}
