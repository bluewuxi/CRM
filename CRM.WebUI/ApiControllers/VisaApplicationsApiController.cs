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
using CRM.Domain.Concrete;
using CRM.WebUI.Models;

namespace CRM.WebUI.ApiControllers
{
    [Produces("application/json")]
    //[Route("api/VisaApplicationsApi")]
    public class VisaApplicationsApiController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }

        private IVisaApplicationRepository _Repo;

        public VisaApplicationsApiController(UserManager<ApplicationUser> aUserManager, IVisaApplicationRepository aRepo) : base(aUserManager)
        {
            _Repo = aRepo;
        }

        [HttpGet("api/VisaApplications")]
        public async Task<IActionResult> ListStudent(int limit = 0, int offset = 0, string search = "", string sort = "", string order = "")
        {
            IQueryable<VisaApplication> records;
            List<QuerySetting> aSearch = null;
            List<QuerySetting> aSort = null;

            BindUserContext(_Repo);

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

        [HttpGet("api/VisaApplications/student/{id}")]
        public IActionResult StudentVisaApplication([FromRoute] int id)
        {
            IQueryable<VisaApplication> records;

            BindUserContext(_Repo);
            records = _Repo.GetAll().Where(a => a.StudentID == id).OrderByDescending(a => a.CreatedTime);
            return Json(records);
        }

        private IActionResult Json(IQueryable<ApplicationUser> queryable, object allowGet)
        {
            throw new NotImplementedException();
        }
    }


}