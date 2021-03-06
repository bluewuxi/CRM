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
using Microsoft.AspNetCore.Authorization;

namespace CRM.WebUI.Controllers
{
    [Authorize]
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
            return View();
        }

        // GET: Leads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BindUserContext(_repo);
            var lead = await _repo.GetAsync(id.GetValueOrDefault());
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }

        // GET: Leads/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "UserName");

            return View(new Lead() { CustomerOwnerID = await GetUserContextAsync() });
        }

        // POST: Leads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Source,CustomerID,Name,PreferName,Gender,Birthdate,AcademicBackground,EMail,Mobile,Address,Note,CustomerOwnerID")] Lead lead)
        {
            if (ModelState.IsValid)
            {
                BindUserContext(_repo);
                await _repo.AddAsync(lead);
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

            BindUserContext(_repo);
            var lead = await _repo.GetAsync( id.GetValueOrDefault());
            if (lead == null)
            {
                return NotFound();
            }
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "UserName", lead.CustomerOwnerID);
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
                    BindUserContext(_repo);
                    await _repo.UpdateAsync(lead);
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
            ViewData["CustomerOwnerID"] = new SelectList(_context.Users, "Id", "UserName", lead.CustomerOwnerID);
            return View(lead);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BindUserContext(_repo);
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool LeadExists(int id)
        {
            return _repo.Get(id)==null? false:true;
        }
    }
}
