using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.Domain.Concrete;
using CRM.Domain.Entities;
using Newtonsoft.Json;
using CRM.WebUI.Models;
using CRM.Domain.Abstract;
using Microsoft.AspNetCore.Identity;

namespace CRM.WebUI.ApiControllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersApiController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }

        private ICustomerRepository _Repo;

        public CustomersApiController(UserManager<ApplicationUser> aUserManager, ICustomerRepository aRepo) : base(aUserManager)
        {
            _Repo = aRepo;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> ListCustomers(int limit = 0, int offset = 0, string search = "", string sort = "", string order = "")
        {
            IQueryable<Customer> records;
            List<QuerySetting> aSearch = null;
            List<QuerySetting> aSort = null;

            if (!(search == null || search == ""))
            {
                aSearch = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
            }
            if (!(sort == null || sort == ""))
            {
                aSort = JsonConvert.DeserializeObject<List<QuerySetting>>(sort);
            }

            BindUserContext(_Repo);

            records = _Repo.GetAll(aSearch, aSort);
            var count = await records.CountAsync();

            if (limit <= 0)
                return Json(records);
            else
                return Json(new { total = count, rows = records.Skip(offset).Take(limit) });
        }
    }
}