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
        private IAccountRepository _accountRepo;
        //private IQueryable<ApplicationUser> _userList;
        //private UserManager<ApplicationUser> _userManager;

        public AccountsController(IAccountRepository accountRepo, UserManager<ApplicationUser> userManager)
        {
            _accountRepo = accountRepo;
            AttachUserManager(userManager);
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

            var account = await _accountRepo.GetAsync(id.GetValueOrDefault());
                
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
        public async Task<IActionResult> Create([Bind("AccountID,Name,PreferName,Gender,Birthdate,Nationality,PassportNumber,EMail,Mobile,RegisterDate,Address,Note,AccountType,AccountOwner")] Account account)
        {
            if (ModelState.IsValid)
            {
                _accountRepo.AttachUserContext(GetCurrentUserID());
                await _accountRepo.AddAsync(account);
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

            var account = await _accountRepo.GetAsync(id.GetValueOrDefault());
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
        public async Task<IActionResult> Edit(int id, [Bind("AccountID,Name,ShortName,PreferName,Gender,Birthdate,Nationality,PassportNumber,EMail,Mobile,RegisterDate,Address,Note, AccountType,AccountOwnerID")] Account account)
        {
            if (id != account.AccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _accountRepo.AttachUserContext(GetCurrentUserID());
                    await _accountRepo.UpdateAsync(account);
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

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _accountRepo.GetAsync(id.GetValueOrDefault());
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _accountRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool AccountExists(int id)
        {
            return _accountRepo.GetAll().Any(e => e.AccountID == id);
        }

        //private Task<ApplicationUser> GetCurrentUserAsync()
        //{
        //    if (HttpContext.User == null) return null;
        //    return _userManager.GetUserAsync(HttpContext.User);
        //}

    }
}
