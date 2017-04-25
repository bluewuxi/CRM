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
    public class EnrollmentsApiController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }

        private IEnrollmentRepository _Repo;

        public EnrollmentsApiController(UserManager<ApplicationUser> aUserManager, IEnrollmentRepository aRepo) : base(aUserManager)
        {
            _Repo = aRepo;
        }

        [HttpGet("api/Enrollments")]
        public async Task<IActionResult> ListStudent(int limit = 10, int offset = 0, string search = "", string sort = "", string order = "")
        {
            IQueryable<Enrollment> records;
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

        [HttpGet("api/Enrollments/student/{id}")]
        public IActionResult StudentEnrollment([FromRoute] int id)
        {
            IQueryable<Enrollment> records;

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