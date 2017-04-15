using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Domain.Concrete;
using CRM.Domain.Entities;

namespace CRM.WebUI.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly EFDbContext _context;

        public EnrollmentsController(EFDbContext context)
        {
            _context = context;    
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var eFDbContext = _context.Enrollments.Include(e => e.CreatedBy).Include(e => e.EnrollmentAgent).Include(e => e.Institute).Include(e => e.ModifiedBy).Include(e => e.Student);
            return View(await eFDbContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.CreatedBy)
                .Include(e => e.EnrollmentAgent)
                .Include(e => e.Institute)
                .Include(e => e.ModifiedBy)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["EnrollmentAgentID"] = new SelectList(_context.Accounts, "AccountID", "Name");
            ViewData["InstituteID"] = new SelectList(_context.Accounts, "AccountID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentID,StudentID,InstituteID,PaymentDate,DueDate,EndDate,Tuition,EnrollmentAgentID,Status,Note,ModifiedTime,CreatedTime,ModifiedByID,CreatedByID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EnrollmentAgentID"] = new SelectList(_context.Accounts, "AccountID", "Name", enrollment.EnrollmentAgentID);
            ViewData["InstituteID"] = new SelectList(_context.Accounts, "AccountID", "Name", enrollment.InstituteID);
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["EnrollmentAgentID"] = new SelectList(_context.Accounts, "AccountID", "Name", enrollment.EnrollmentAgentID);
            ViewData["InstituteID"] = new SelectList(_context.Accounts, "AccountID", "Name", enrollment.InstituteID);
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentID,StudentID,InstituteID,PaymentDate,DueDate,EndDate,Tuition,EnrollmentAgentID,Status,Note,ModifiedTime,CreatedTime,ModifiedByID,CreatedByID")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
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
            ViewData["EnrollmentAgentID"] = new SelectList(_context.Accounts, "AccountID", "Name", enrollment.EnrollmentAgentID);
            ViewData["InstituteID"] = new SelectList(_context.Accounts, "AccountID", "Name", enrollment.InstituteID);
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.CreatedBy)
                .Include(e => e.EnrollmentAgent)
                .Include(e => e.Institute)
                .Include(e => e.ModifiedBy)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(m => m.EnrollmentID == id);
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }
    }
}
