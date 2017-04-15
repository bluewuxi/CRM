using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM.Domain.Entities;
using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CRM.WebUI.Models;

namespace CRM.WebUI.ApiControllers
{
    [Produces("application/json")]
    public class QueryCodesController : Controller
    {
        public object JsonRequestBehavior { get; private set; }

        [HttpGet("api/QueryCodes/users")]
        public async Task<IActionResult> UsersCode([FromServices] UserManager<ApplicationUser> userManager, int limit = 10, int offset = 0, string search = "", string sort = "", string order = "")
        {
            IQueryable<ApplicationUser> records;
            if (search == null || search == "")
                records = userManager.Users.AsQueryable();
            else
                records = userManager.Users.AsQueryable().Where(u => u.UserName.ToLower().Contains(search.ToLower()));
            var count = await records.CountAsync();
            if (limit <= 0) 
                return Json(records.Select(u => new { Id = u.Id, UserName = u.UserName, Email = u.Email, PhoneNumber = u.PhoneNumber }));
            else
                return Json(new { total= count, rows=(records.Skip(offset).Take(limit)
                    .Select(u => new { Id = u.Id, UserName = u.UserName, Email = u.Email, PhoneNumber = u.PhoneNumber })) });
        }

        [HttpGet("api/QueryCodes/accounts")]
        public async Task<IActionResult> AccountsCode([FromServices] IAccountRepository accountRepo, int limit = 10, int offset = 0, string search = "", string sort = "", string order = "")
        {
            IQueryable<Account> records;
            records = accountRepo.GetAll();
            if (!(search == null || search == ""))
            {
                List<QuerySetting> aSearch = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
                if (aSearch != null && aSearch.Count()>0)
                {
                    string searchName, searchType, searchOwner;
                    searchName = aSearch.Where<QuerySetting>(u => u.field == "AccountName").Select(p => p.value).SingleOrDefault();
                    if (searchName != null && searchName != "") records = records.Where(u => u.Name.ToLower().Contains(searchName.ToLower()));
                    searchType = aSearch.Where<QuerySetting>(u => u.field == "AccountType").Select(p => p.value).SingleOrDefault();

                    //if (searchType != null && searchType != "") records = records.Where(u => u.AccountType== aEnum);
                    if (searchType != null && searchType != "") records = records.Where(u => (int)u.AccountType == int.Parse(searchType));
                    searchOwner = aSearch.Where<QuerySetting>(u => u.field == "AccountOwner").Select(p => p.value).SingleOrDefault();
                    if (searchOwner != null && searchOwner != "") records = records.Where(u => u.AccountOwner.UserName.ToLower().Contains(searchOwner.ToLower()));
                }
            }
            var count = await records.CountAsync();

            if (limit <= 0)
                return Json(records);
            else
                return Json(new {total=count, rows= records.Skip(offset).Take(limit)});
        }

        private IActionResult Json(IQueryable<ApplicationUser> queryable, object allowGet)
        {
            throw new NotImplementedException();
        }
    }


}