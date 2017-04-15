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
    //[Route("api/LeadsApi")]
    public class LeadsApiController : Controller
    {
        public object JsonRequestBehavior { get; private set; }

        [HttpGet("api/leads")]
        public async Task<IActionResult> ListLead([FromServices] ILeadRepository leadRepo, int limit = 10, int offset = 0, string search = "", string sort = "", string order = "")
        {
            IQueryable<Lead> records;
            records = leadRepo.GetAll();
            if (!(search == null || search == ""))
            {
                List<QuerySetting> aSearch = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
                if (aSearch != null && aSearch.Count() > 0)
                {
                    string searchName, searchOwner;
                    searchName = aSearch.Where<QuerySetting>(u => u.field == "LeadName").Select(p => p.value).SingleOrDefault();
                    if (searchName != null && searchName != "") records = records.Where(u => u.Name.ToLower().Contains(searchName.ToLower()) || u.PreferName.ToLower().Contains(searchName.ToLower()));
                    searchOwner = aSearch.Where<QuerySetting>(u => u.field == "AccountOwner").Select(p => p.value).SingleOrDefault();
                    if (searchOwner != null && searchOwner != "") records = records.Where(u => u.CustomerOwner.UserName.ToLower().Contains(searchOwner.ToLower()));
                }
            }
            var count = await records.CountAsync();

            if (limit <= 0)
                return Json(records);
            else
                return Json(new { total = count, rows = records.Skip(offset).Take(limit) });
        }

        private IActionResult Json(IQueryable<ApplicationUser> queryable, object allowGet)
        {
            throw new NotImplementedException();
        }
    }


}