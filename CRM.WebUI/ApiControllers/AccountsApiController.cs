using CRM.Domain.Abstract;
using CRM.Domain.Concrete;
using CRM.Domain.Entities;
using CRM.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WebUI.ApiControllers
{
    [Authorize]
    [Produces("application/json")]
    public class AccountsController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }

        private IAccountRepository _Repo;

        public AccountsController(UserManager<ApplicationUser> aUserManager, IAccountRepository aRepo) : base(aUserManager)
        {
            _Repo = aRepo;
        }

        [HttpGet("api/accounts")]
        public async Task<IActionResult> ListAccounts(int limit = 0, int offset = 0, string search = "", string sort = "")
        {
            IQueryable<Account> records;
            List<QuerySetting> aSearch = null;
            List<QuerySetting> aSort = null;

            //BindUserContext(_Repo);
            if (!(search == null || search == ""))
            {
                aSearch = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
            }
            if (!(sort == null || sort == ""))
            {
                aSort = JsonConvert.DeserializeObject<List<QuerySetting>>(sort);
            }

            string userLimitation = await GetUserLimitation();
            if (userLimitation != "")
            {
                if (aSearch == null) aSearch = new List<QuerySetting>();
                aSearch.Add(new QuerySetting("Owner", userLimitation));
            }

            records = _Repo.GetAll(aSearch, aSort);
            var count = await records.CountAsync();

            if (limit <= 0)
                return Json(records);
            else
                return Json(new {total=count, rows= records.Skip(offset).Take(limit)});
        }

        //For searching and filling agent code, used by agent list modal
        [HttpGet("api/agents")]
        public async Task<IActionResult> ListAgents()
        {
            IQueryable<Account> records;
            List<QuerySetting> aSearch;
            string userLimitation = await GetUserLimitation();
            if (userLimitation != "")
            {
                aSearch = new List<QuerySetting>();
                aSearch.Add(new QuerySetting("Owner", userLimitation));
                records = _Repo.GetAll(aSearch, null).Where(a => a.AccountType == Account.AccountTypeEnum.Agent);
            }
            else
                records = _Repo.GetAll().Where(a=>a.AccountType==Account.AccountTypeEnum.Agent);
            return Json(records);
        }

        //For searching and filling agent code, used by agent list modal
        [HttpGet("api/institutes")]
        public IActionResult ListInstitutes()
        {
            IQueryable<Account> records;
            records = _Repo.GetAll().Where(a => a.AccountType == Account.AccountTypeEnum.Institute);
            return Json(records);
        }

        private IActionResult Json(IQueryable<ApplicationUser> queryable, object allowGet)
        {
            throw new NotImplementedException();
        }
    }


}