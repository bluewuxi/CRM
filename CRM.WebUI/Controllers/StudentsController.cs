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
    public class StudentsController : BaseController
    {
        private readonly EFDbContext _context;

        private readonly IStudentRepository _repo;

        public StudentsController(EFDbContext context, IStudentRepository repo, UserManager<ApplicationUser> userManager):base(userManager)
        {
            _repo = repo;
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var eFDbContext = _context.Students.Include(s => s.CreatedBy).Include(s => s.CustomerOwner).Include(s => s.ModifiedBy).Include(s => s.Agent);
            return View(await eFDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.CreatedBy)
                .Include(s => s.CustomerOwner)
                .Include(s => s.ModifiedBy)
                .Include(s => s.Agent)
                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["AgentID"] = new SelectList(_context.Accounts, "AccountID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rating,Nationality,PassportNumber,ClientNumber,ContactName,AgentID,CustomerID,Name,PreferName,Gender,Birthdate,AcademicBackground,EMail,Mobile,Address,Note,CustomerOwnerID,ModifiedTime,CreatedTime,ModifiedByID,CreatedByID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "Id", student.CustomerOwnerID);
            ViewData["AgentID"] = new SelectList(_context.Accounts, "AccountID", "Name", student.AgentID);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.CustomerID == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "Id", student.CustomerOwnerID);
            ViewData["AgentID"] = new SelectList(_context.Accounts, "AccountID", "Name", student.AgentID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rating,Nationality,PassportNumber,ClientNumber,ContactName,AgentID,CustomerID,Name,PreferName,Gender,Birthdate,AcademicBackground,EMail,Mobile,Address,Note,CustomerOwnerID,ModifiedTime,CreatedTime,ModifiedByID,CreatedByID")] Student student)
        {
            if (id != student.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.CustomerID))
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
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "Id", student.CustomerOwnerID);
            ViewData["AgentID"] = new SelectList(_context.Accounts, "AccountID", "Name", student.AgentID);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.CreatedBy)
                .Include(s => s.CustomerOwner)
                .Include(s => s.ModifiedBy)
                .Include(s => s.Agent)
                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.CustomerID == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.CustomerID == id);
        }
    }
}
