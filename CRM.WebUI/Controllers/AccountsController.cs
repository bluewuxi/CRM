using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CRM.WebUI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using CRM.Domain.Concrete;

namespace CRM.WebUI.Controllers
{
    public static class AccountQueryExtender
    {
        public static IQueryable<Account> ApplyFilter(this IQueryable<Account> c, string Filter="")
        {
            if (Filter == null || Filter == "") return c;
            string nameFilter = Filter.ToLower();
            return c.Where(p => p.Name.ToLower().Contains(nameFilter));
        }
    }

    public class AccountsController : BaseController
    {
        //private readonly EFDbContext _context;
        private IAccountRepository _repo;

        public AccountsController(IAccountRepository accountRepo, UserManager<ApplicationUser> userManager):base(userManager)
        {
            _repo = accountRepo;
        }

        // GET: Accounts
        public IActionResult Index(string Filter="", string Sorter="")
        {
            //We use RESPful WebApi do list accounts, leave here empty.
            var querySetting = HttpContext.Session.Get<QuerySettingViewModel>("AccountsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            return View(querySetting);
        }

        // POST: Set Accounts Filter
        [HttpPost]
        public void SetQuery(string search = "", string sort = "", long offset=0)
        {
            QuerySettingViewModel querySetting = HttpContext.Session.Get<QuerySettingViewModel>("AccountsList");
            if (querySetting == null)
                querySetting = new QuerySettingViewModel();
            else
                querySetting.search.Clear();

            if (search !=null && search != "")
                querySetting.search= JsonConvert.DeserializeObject<List<QuerySetting>>(search);
            HttpContext.Session.Set<QuerySettingViewModel>("AccountsList", querySetting);
            string a = HttpContext.Session.GetString("AccountsList");
            Response.Redirect("/Accounts/Index");
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BindUserContext(_repo);
            var account = await _repo.GetAsync(id.GetValueOrDefault());
                
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser curUser = await GetCurrentUserAsync(); 
            ViewData["Userlist"] = new SelectList(GetUsers(), "Id", "UserName");
            Account newAccount = new Account();
            if (curUser!=null)
                newAccount.AccountOwnerID = curUser.Id;
            return View(newAccount);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountID,Name,PreferName,ContactGender,Birthdate,Nationality,PassportNumber,EMail,Mobile,RegisterDate,Address,Note,AccountType,AccountOwner")] Account account)
        {
            if (ModelState.IsValid)
            {
                BindUserContext(_repo);
                await _repo.AddAsync(account);
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Userlist"] = new SelectList(GetUsers(), "Id", "UserName");

            BindUserContext(_repo);
            var account = await _repo.GetAsync(id.GetValueOrDefault());
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountID,Name,ShortName,PreferName,ContactGender,Birthdate,Nationality,PassportNumber,EMail,Mobile,RegisterDate,Address,Note, AccountType,AccountOwnerID")] Account account)
        {
            if (id != account.AccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    BindUserContext(_repo);
                    await _repo.UpdateAsync(account);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountID))
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
            return View(account);
        }


        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BindUserContext(_repo);
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool AccountExists(int id)
        {
            return _repo.GetAll().Any(e => e.AccountID == id);
        }

    }
}
