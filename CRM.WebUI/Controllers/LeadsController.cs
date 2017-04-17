using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Domain.Concrete;
using CRM.Domain.Entities;
using CRM.WebUI.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity;

namespace CRM.WebUI.Controllers
{
    public class LeadsController : BaseController
    {
        private readonly EFDbContext _context;

        private readonly ILeadRepository _repo;

        public LeadsController(EFDbContext context, ILeadRepository repo, UserManager<ApplicationUser> userManager):base(userManager)
        {
            _repo = repo;
            _context = context;
        }

        // GET: Leads
        public IActionResult Index()
        {
            //We use RESPful WebApi do list accounts, leave here empty.
            var querySetting = HttpContext.Session.Get<QuerySettingViewModel>("LeadsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            return View(querySetting);
        }

        // POST: Set Accounts Filter
        [HttpPost]
        public void SetQuery(string search = "", string sort = "", long offset = 0)
        {
            QuerySettingViewModel querySetting = HttpContext.Session.Get<QuerySettingViewModel>("LeadsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            else
                querySetting.search.Clear();

            if (search != null && search != "")
                querySetting.search = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
            HttpContext.Session.Set<QuerySettingViewModel>("LeadsList", querySetting);
            string a = HttpContext.Session.GetString("LeadsList");
            Response.Redirect("/Leads/Index");
        }

        // GET: Leads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads
                .Include(l => l.CreatedBy)
                .Include(l => l.CustomerOwner)
                .Include(l => l.ModifiedBy)
                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }

        // GET: Leads/Create
        public IActionResult Create()
        {
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Source,CustomerID,Name,PreferName,Gender,Birthdate,AcademicBackground,EMail,Mobile,Address,Note,CustomerOwnerID,ModifiedTime,CreatedTime,ModifiedByID,CreatedByID")] Lead lead)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lead);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "UserName", lead.CustomerOwnerID);
            return View(lead);
        }

        // GET: Leads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads.SingleOrDefaultAsync(m => m.CustomerID == id);
            if (lead == null)
            {
                return NotFound();
            }
            ViewData["CreatedByID"] = new SelectList(_context.Users, "Id", "Id", lead.CreatedByID);
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "Id", lead.CustomerOwnerID);
            ViewData["ModifiedByID"] = new SelectList(_context.Users, "Id", "Id", lead.ModifiedByID);
            return View(lead);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Source,CustomerID,Name,PreferName,Gender,Birthdate,AcademicBackground,EMail,Mobile,Address,Note,CustomerOwnerID,ModifiedTime,CreatedTime,ModifiedByID,CreatedByID")] Lead lead)
        {
            if (id != lead.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lead);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadExists(lead.CustomerID))
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
            ViewData["CreatedByID"] = new SelectList(_context.Users, "Id", "Id", lead.CreatedByID);
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "Id", lead.CustomerOwnerID);
            ViewData["ModifiedByID"] = new SelectList(_context.Users, "Id", "Id", lead.ModifiedByID);
            return View(lead);
        }

        // GET: Leads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads
                .Include(l => l.CreatedBy)
                .Include(l => l.CustomerOwner)
                .Include(l => l.ModifiedBy)
                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lead = await _context.Leads.SingleOrDefaultAsync(m => m.CustomerID == id);
            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LeadExists(int id)
        {
            return _context.Leads.Any(e => e.CustomerID == id);
        }
    }
}
