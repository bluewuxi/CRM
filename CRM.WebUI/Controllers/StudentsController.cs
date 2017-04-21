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
            //We use RESPful WebApi do list accounts, leave here empty.
            var querySetting = HttpContext.Session.Get<QuerySettingViewModel>("StudentsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            return View(querySetting);
        }

        // POST: Set Accounts Filter
        [HttpPost]
        public void SetQuery(string search = "", string sort = "", long offset = 0)
        {
            QuerySettingViewModel querySetting = HttpContext.Session.Get<QuerySettingViewModel>("StudentsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            else
                querySetting.search.Clear();

            if (search != null && search != "")
                querySetting.search = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
            HttpContext.Session.Set<QuerySettingViewModel>("StudentsList", querySetting);
            string a = HttpContext.Session.GetString("StudentsList");
            Response.Redirect("/Students/Index");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _repo.GetAsync(id.GetValueOrDefault());
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
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
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.CustomerID == id);
        }
    }
}
