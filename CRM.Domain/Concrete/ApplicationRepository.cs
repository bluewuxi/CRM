using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Concrete
{
    public class ApplicationRepository: BaseRepository, IApplicationRepository
    {
        private EFDbContext _context;
        private DbSet<Application> _entity;

        public ApplicationRepository(EFDbContext dbcontext)
        {
            this._context = dbcontext;
            _entity = _context.Set<Application>();
        }

        public Application Get(int id)
        {
            return _entity.Include(u => u.Institute).Include(s=>s.Student)
                .Include(a=>a.ApplicationAgent)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefault(s => s.ApplicationID == id);
        }
        public async Task<Application> GetAsync(int id)
        {
            return await _entity.Include(u => u.Institute)
                .Include(s => s.Student)
                .Include(a => a.ApplicationAgent)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefaultAsync(s => s.ApplicationID == id);
        }

        public IQueryable<Application> GetAll()
        {
            return _entity.Include(u => u.Institute).Include(s => s.Student).Include(a => a.ApplicationAgent).AsQueryable();
        }

        public IQueryable<Application> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            string sStudentName, sInstituteName, sAgentName, sStatus, sSubmittedDate, sOwner;

            IQueryable<Application> records = _entity.Include(u => u.Institute).Include(s => s.Student).Include(a => a.ApplicationAgent);

            if (search != null && search.Count() > 0)
            {
                sOwner = search.Where(u => u.Field == "Owner").Select(p => p.Value).SingleOrDefault();
                if (sOwner != null && sOwner != "") records = records.Where(u => u.Student.CustomerOwnerID == sOwner || u.Student.ModifiedByID == sOwner || u.Student.CustomerOwnerID == null);

                sStudentName = search.Where<QuerySetting>(u => u.Field == "StudentName").Select(p => p.Value).SingleOrDefault();
                if (sStudentName != null && sStudentName != "") records = records.Where(u => u.Student.Name.ToLower().Contains(sStudentName.ToLower())
                    || u.Student.PreferName.ToLower().Contains(sStudentName.ToLower()));

                sInstituteName = search.Where<QuerySetting>(u => u.Field == "InstituteName").Select(p => p.Value).SingleOrDefault();
                if (sInstituteName != null && sInstituteName != "") records = records.Where(u => u.Institute.Name.ToLower().Contains(sInstituteName.ToLower())
                    || u.Institute.ShortName.ToLower().Contains(sInstituteName.ToLower()));

                sAgentName = search.Where<QuerySetting>(u => u.Field == "AgentName").Select(p => p.Value).SingleOrDefault();
                if (sAgentName != null && sAgentName != "") records = records.Where(u => u.ApplicationAgent.Name.ToLower().Contains(sAgentName.ToLower())
                    || u.ApplicationAgent.ShortName.ToLower().Contains(sAgentName.ToLower()));

                sStatus = search.Where<QuerySetting>(u => u.Field == "Status").Select(p => p.Value).SingleOrDefault();
                if (sStatus != null && sStatus != "") records = records.Where(u => (int)u.Status == int.Parse(sStatus));

                sSubmittedDate = search.Where<QuerySetting>(u => u.Field == "SubmittedDate").Select(p => p.Value).SingleOrDefault();
                if (sSubmittedDate != null && sSubmittedDate != "")
                {
                    DatetimeRange r = new DatetimeRange(sSubmittedDate);
                    if (r.dBegin != null) records = records.Where(u => u.SubmittedDate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.SubmittedDate <= r.dEnd);
                }
            }

            return records;
        }

        public async Task<int> UpdateAsync(Application Item)
        {
            await CreateActivity(Item);
            SetModifiedSignature(Item);
            _context.Entry(Item).State= EntityState.Modified;
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAsync(Application application)
        {
            await CreateActivity(application);
            SetCreatedSignature(application);
            _context.Entry(application).State = EntityState.Added;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Application item = Get(id);
            _entity.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public Task<IQueryable<Application>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<Activity> CreateActivity(Application application)
        {
            if (application.SubmittedDate == null || application.Status != Application.ApplicationStatusEnum.Applied) return null;

            if (application.ApplicationID != 0) // Check if SubmittedDate is changed when it is not a new application
            {
                Application current = await _entity.AsNoTracking()
                        .SingleOrDefaultAsync(x => x.ApplicationID == application.ApplicationID);
                if (current.SubmittedDate != null && current.SubmittedDate == application.SubmittedDate
                    && current.Status == Application.ApplicationStatusEnum.Applied)
                    return null;
            }

            Activity a = new Activity()
            {
                Status = Activity.ActivityStatusEnum.OpenTask,
                ActivityType = Activity.ActivityTypeEnum.Other,
                Subject = "[System] Follow Institute Application ",
                StartTime = application.SubmittedDate.GetValueOrDefault().AddDays(7),
                AttendedAccountID = application.InstituteID,
                AttendedCustomerID = application.StudentID,
            };
            SetCreatedSignature(a);

            // Set activity owner as the student owner or if student owner is empty, set the current user.
            Student s = await _context.Students.Where(x => x.CustomerID == application.StudentID).AsNoTracking().SingleOrDefaultAsync();
            a.ActivityOwnerID = (s.CustomerOwnerID == null || s.CustomerOwnerID == "") ? a.ModifiedByID : s.CustomerOwnerID;
            _context.Entry(a).State= EntityState.Added;
            return a;

        }
    }
}
