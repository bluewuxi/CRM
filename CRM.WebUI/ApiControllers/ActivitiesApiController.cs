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
    //[Route("api/LeadsApi")]
    public class ActivitiesApiController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }
        private IActivityRepository _Repo;

        public ActivitiesApiController(UserManager<ApplicationUser> aUserManager, IActivityRepository aRepo) : base(aUserManager)
        {
            _Repo = aRepo;
        }

        [HttpGet("api/activities/leads/{id}/{status}")]
        public IActionResult ListLeadActivities([FromRoute] int id, [FromRoute] string status="")
        {
            IQueryable<Activity> records;
            records = _Repo.GetAll().Where(u=>u.AttendedCustomer.CustomerID==id).OrderBy(s => s.StartTime);

            if (status.Trim().ToLower() == "open")
                records = records.Where(u => u.Status == Activity.ActivityStatusEnum.OpenTask || 
                        (u.EndTime >= DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event) );

            if (status.Trim().ToLower() == "closed")
                records = records.Where(u => u.Status == Activity.ActivityStatusEnum.ClosedTask ||
                        (u.EndTime < DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));

            return Json(records);
        }

        [HttpGet("api/activities/student/{id}/{status}")]
        public IActionResult ListStudentActivities([FromRoute] int id, [FromRoute] string status="")
        {
            IQueryable<Activity> records;
            records = _Repo.GetAll().Where(u => u.AttendedCustomer.CustomerID == id).OrderBy(s=>s.StartTime);

            if (status.Trim().ToLower() == "open")
                records = records.Where(u => u.Status == Activity.ActivityStatusEnum.OpenTask ||
                        (u.EndTime >= DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));

            if (status.Trim().ToLower() == "closed")
                records = records.Where(u => u.Status == Activity.ActivityStatusEnum.ClosedTask ||
                        (u.EndTime < DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));
            return Json(records);
        }

        [HttpGet("api/activities/accounts/{id}/{status}")]
        public IActionResult ListAccountActivities([FromRoute] int id, [FromRoute] string status="")
        {
            IQueryable<Activity> records;
            records = _Repo.GetAll().Where(u => u.AttendedAccount.AccountID == id).OrderBy(s => s.StartTime);

            if (status.Trim().ToLower() == "open")
                records = records.Where(u => u.Status == Activity.ActivityStatusEnum.OpenTask ||
                        (u.EndTime >= DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));

            if (status.Trim().ToLower() == "closed")
                records = records.Where(u => u.Status == Activity.ActivityStatusEnum.ClosedTask ||
                        (u.EndTime < DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));

            return Json(records);
        }


        [HttpGet("api/activities/my/{status}")]
        public async Task<IActionResult> ListMyActivities([FromRoute] string status = "")
        {
            IQueryable<Activity> records;
            ApplicationUser user = await GetCurrentUserAsync();
            if (user == null) return NotFound();

            records = _Repo.GetAll().Where(u => u.ActivityOwnerID == user.Id ).OrderBy(s => s.StartTime);

            if (status.Trim().ToLower() == "open")
                records = records.Where(u => u.Status == Activity.ActivityStatusEnum.OpenTask ||
                        (u.EndTime >= DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));

            if (status.Trim().ToLower() == "closed")
                records = records.Where(u => u.Status == Activity.ActivityStatusEnum.ClosedTask ||
                        (u.EndTime < DateTime.Now && u.Status == Activity.ActivityStatusEnum.Event));

            return Json(records);
        }


        [HttpGet("api/activities")]
        public async Task<IActionResult> ListActivities(int limit = 0, int offset = 0, string search = "", string sort = "")
        {
            IQueryable<Activity> records;
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
            {
                return Json(new { total = count, rows = records.Skip(offset).Take(limit) });
            }
        }

        // PUT: api/Activity/5
        [HttpPut("api/activities/detail/{id}")]
        public async Task<IActionResult> PutActivity([FromRoute] int id, [FromBody] Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activity.ActivityID)
            {
                return BadRequest();
            }

            try
            {
                await _Repo.UpdateAsync(activity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Activity
        [HttpPost]
        public async Task<IActionResult> PostActivity([FromBody] Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _Repo.AddAsync(activity);

            return CreatedAtAction("GetActivity", new { id = activity.ActivityID }, activity);
        }

        // DELETE: api/Activity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _Repo.DeleteAsync(id));

        }

        private bool ActivityExists(int id)
        {
            return _Repo.Get(id)!=null;
        }
        private IActionResult Json(IQueryable<ApplicationUser> queryable, object allowGet)
        {
            throw new NotImplementedException();
        }
    }


}