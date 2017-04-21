using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Domain.Concrete;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using CRM.Domain.Abstract;
using CRM.WebUI.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace CRM.WebUI.Controllers
{
    public class ApplicationsController : BaseController
    {
        private readonly EFDbContext _context;
        private readonly IApplicationRepository _repo;

        public ApplicationsController(EFDbContext context, IApplicationRepository repo, UserManager<ApplicationUser> userManager):base(userManager)
        {
            _repo = repo;
            _context = context;    
        }

        // GET: Applications
        public IActionResult Index()
        {
            var eFDbContext = _context.Applications.Include(a => a.ApplicationAgent).Include(a => a.CreatedBy).Include(a => a.Institute).Include(a => a.ModifiedBy).Include(a => a.Student);
            
            //We use RESPful WebApi do list accounts, leave here empty.
            var querySetting = HttpContext.Session.Get<QuerySettingViewModel>("ApplicatiosnList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            return View(querySetting);
        }

        // POST: Set Application Filter
        [HttpPost]
        public void SetQuery(string search = "", string sort = "", long offset = 0)
        {
            QuerySettingViewModel querySetting = HttpContext.Session.Get<QuerySettingViewModel>("ApplicationsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            else
                querySetting.search.Clear();

            if (search != null && search != "")
                querySetting.search = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
            HttpContext.Session.Set<QuerySettingViewModel>("ApplicationsList", querySetting);
            string a = HttpContext.Session.GetString("ApplicationsList");
            Response.Redirect("/Applications/Index");
        }


        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.ApplicationAgent)
                .Include(a => a.CreatedBy)
                .Include(a => a.Institute)
                .Include(a => a.ModifiedBy)
                .Include(a => a.Student)
                .SingleOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["ApplicationAgentID"] = new SelectList(_context.Accounts.Where(a=>a.AccountType==Account.AccountTypeEnum.Agent), "AccountID", "Name");
            ViewData["InstituteID"] = new SelectList(_context.Accounts.Where(a => a.AccountType == Account.AccountTypeEnum.Institute), "AccountID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name");

            Application newRecord = new Application() { Status = Application.ApplicationStatusEnum.Draft };
            return View(newRecord);
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationID,StudentID,InstituteID,Tuition,ApplicationAgentID,Status,Note,ModifiedTime")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.SingleOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["ApplicationAgentID"] = new SelectList(_context.Accounts.Where(a => a.AccountType == Account.AccountTypeEnum.Agent), "AccountID", "Name");
            ViewData["InstituteID"] = new SelectList(_context.Accounts.Where(a => a.AccountType == Account.AccountTypeEnum.Institute), "AccountID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name", application.StudentID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationID,StudentID,InstituteID,Tuition,ApplicationAgentID,Status,Note,ModifiedTime,CreatedTime,ModifiedByID,CreatedByID")] Application application)
        {
            if (id != application.ApplicationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationID))
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
            ViewData["ApplicationAgentID"] = new SelectList(_context.Accounts.Where(a => a.AccountType == Account.AccountTypeEnum.Agent), "AccountID", "Name");
            ViewData["InstituteID"] = new SelectList(_context.Accounts.Where(a => a.AccountType == Account.AccountTypeEnum.Institute), "AccountID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students, "CustomerID", "Name", application.StudentID);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.ApplicationAgent)
                .Include(a => a.CreatedBy)
                .Include(a => a.Institute)
                .Include(a => a.ModifiedBy)
                .Include(a => a.Student)
                .SingleOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.SingleOrDefaultAsync(m => m.ApplicationID == id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationID == id);
        }
    }
}
