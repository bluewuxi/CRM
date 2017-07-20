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
    //[Route("api/StudentsApi")]
    public class StudentsApiController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }

        private IStudentRepository _Repo;

        public StudentsApiController(UserManager<ApplicationUser> aUserManager, IStudentRepository aRepo) : base(aUserManager)
        {
            _Repo = aRepo;
        }

        [HttpGet("api/students")]
        public async Task<IActionResult> ListStudent(int limit = 0, int offset = 0, string search = "", string sort = "", string order = "")
        {
            IQueryable<Student> records;
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
                return Json(new { total = count, rows = records.Skip(offset).Take(limit) });
        }

        private IActionResult Json(IQueryable<ApplicationUser> queryable, object allowGet)
        {
            throw new NotImplementedException();
        }
    }


}