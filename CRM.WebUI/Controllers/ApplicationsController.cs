using CRM.Domain.Abstract;
using CRM.Domain.Concrete;
using CRM.Domain.Entities;
using CRM.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WebUI.Controllers
{
    [Authorize]
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
            //We use RESPful WebApi do list , leave here empty.
            return View();
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BindUserContext(_repo);
            var application = await _repo.GetAsync(id.GetValueOrDefault());
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            Application newRecord = new Application() { Status = Application.ApplicationStatusEnum.Draft };
            return View(newRecord);
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationID,SubmittedDate,StudentID,InstituteID,Tuition,ApplicationAgentID,Status,Note")] Application application)
        {
            if (ModelState.IsValid)
            {
                BindUserContext(_repo);
                await _repo.AddAsync(application);
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

            BindUserContext(_repo);
            var application = await _repo.GetAsync(id.GetValueOrDefault());
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationID,SubmittedDate,Status,StudentID,InstituteID,Tuition,ApplicationAgentID,Status,Note")] Application application)
        {
            if (id != application.ApplicationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    BindUserContext(_repo);
                    await _repo.UpdateAsync(application);
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
            return View(application);
        }


        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BindUserContext(_repo);
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationID == id);
        }
    }
}
