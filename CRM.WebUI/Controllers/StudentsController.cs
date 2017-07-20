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
using Microsoft.AspNetCore.Authorization;

namespace CRM.WebUI.Controllers
{
    [Authorize]
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
        public IActionResult Index()
        {
            //We use RESPful WebApi do list , leave here empty.
            return View();
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BindUserContext(_repo);

            var student = await _repo.GetAsync(id.GetValueOrDefault());
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Convert(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _repo.GetAsync(id.GetValueOrDefault());

            if (student != null) return View(student);

            int x = await _context.Database.ExecuteSqlCommandAsync(
            "update Customers set Discriminator = 'Student', Rating='1',Note = 'Source:' + ISNULL(\"Source\",'') + '; Convert from lead ' + convert(varchar(25), getdate(), 120)+'; '+ISNULL(\"Note\",''), \"Source\" = null where \"CustomerID\" = 1;");

            BindUserContext(_repo);
            student = await _repo.GetAsync(id.GetValueOrDefault());
            if (student == null)
            {
                return NotFound();
            }

            return View("Edit",student);
        }

        // GET: Students/Create
        public async Task<IActionResult> Create()
        {
            Student newStudent = new Student() { CustomerOwnerID = await GetUserContextAsync() };
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "UserName", newStudent.CustomerOwnerID);
            return View(newStudent);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rating,Nationality,PassportNumber,ClientNumber,ContactName,AgentID,CustomerID,Name,PreferName,Gender,Birthdate,AcademicBackground,EMail,Mobile,Address,Note,CustomerOwnerID")] Student student)
        {
            if (ModelState.IsValid)
            {
                BindUserContext(_repo);
                await _repo.AddAsync(student);
                return RedirectToAction("Index");
            }
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "UserName", student.CustomerOwnerID);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BindUserContext(_repo);
            var student = await _repo.GetAsync(id.GetValueOrDefault());
            if (student == null)
            {
                return NotFound();
            }
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "UserName", student.CustomerOwnerID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rating,Nationality,PassportNumber,ClientNumber,ContactName,AgentID,CustomerID,Name,PreferName,Gender,Birthdate,AcademicBackground,EMail,Mobile,Address,Note,CustomerOwnerID")] Student student)
        {
            if (id != student.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    BindUserContext(_repo);
                    await _repo.UpdateAsync(student);
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
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "UserName", student.CustomerOwnerID);
            return View(student);
        }


        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BindUserContext(_repo);
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.CustomerID == id);
        }
    }
}
