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
    //[Route("api/ApplicationsApi")]
    public class ApplicationsApiController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }

        private IApplicationRepository _Repo;

        public ApplicationsApiController(UserManager<ApplicationUser> aUserManager, IApplicationRepository aRepo) : base(aUserManager)
        {
            _Repo = aRepo;
        }

        [HttpGet("api/Applications")]
        public async Task<IActionResult> ListApplication( int limit = 10, int offset = 0, string search = "", string sort = "", string order = "")
        {
            IQueryable<Application> records;
            List<QuerySetting> aSearch = null;
            List<QuerySetting> aSort = null;

            BindUserContext(_Repo);
            string userLimitation = await GetUserLimitation();
            if (userLimitation != "")
            {
                if (aSearch == null) aSearch = new List<QuerySetting>();
                aSearch.Add(new QuerySetting("Owner", userLimitation));
            }

            if (!(search == null || search == ""))
            {
                aSearch = JsonConvert.DeserializeObject<List<QuerySetting>>(search);
            }
            if (!(sort == null || sort == ""))
            {
                aSort = JsonConvert.DeserializeObject<List<QuerySetting>>(sort);
            }

            records = _Repo.GetAll(aSearch, aSort);
            var count = await records.CountAsync();

            if (limit <= 0)
                return Json(records);
            else
                return Json(new { total = count, rows = records.Skip(offset).Take(limit) });
        }

        [HttpGet("api/Applications/student/{id}")]
        public IActionResult StudentApplication([FromRoute] int id)
        {
            IQueryable<Application> records;

            BindUserContext(_Repo);
            records = _Repo.GetAll().Where(a=>a.StudentID==id).OrderByDescending(a=>a.CreatedTime);
            return Json(records);
        }

        private IActionResult Json(IQueryable<ApplicationUser> queryable, object allowGet)
        {
            throw new NotImplementedException();
        }
    }


}