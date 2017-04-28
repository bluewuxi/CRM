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
    public class ActivityRepository: BaseRepository, IActivityRepository
    {
        private EFDbContext _context;
        private DbSet<Activity> activityEntity;

        public ActivityRepository(EFDbContext dbcontext)
        {
            this._context = dbcontext;
            activityEntity = _context.Set<Activity>();
        }

        public int Add(Activity activity)
        {
            _context.Entry(activity).State = EntityState.Added;
            SetCreatedSignature(activity);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            Activity activity = Get(id);
            activityEntity.Remove(activity);
            return _context.SaveChanges();
        }

        public Activity Get(int id)
        {
            //return activityEntity.Include(u => u.AttendedCustomer).Include(u=>u.AttendedAccount).SingleOrDefault(s => s.ActivityID == id);
            return activityEntity
                .Include(a => a.ActivityOwner)
                .Include(a => a.AttendedAccount)
                .Include(a => a.AttendedCustomer)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefault(m => m.ActivityID == id);
        }

        public Task<Activity> GetAsync(int id)
        {
            return activityEntity
                .Include(a => a.ActivityOwner)
                .Include(a => a.AttendedAccount)
                .Include(a => a.AttendedCustomer)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefaultAsync(m => m.ActivityID == id);
        }

        public IQueryable<Activity> GetAll()
        {
            IQueryable<Activity> records = activityEntity.Include(a => a.ActivityOwner).Include(a => a.AttendedAccount).Include(a => a.AttendedCustomer);

            return records;
        }

        public IQueryable<Activity> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            IQueryable<Activity> records = activityEntity.Include(a => a.ActivityOwner).Include(a => a.AttendedAccount).Include(a => a.AttendedCustomer);

            if (search != null && search.Count() > 0)
            {
                string sActivityType, sSubject, sStatus, sActivityOwner, sAttendedAccount, sAttendedCustomer, sStartTime, sEndTime;

                sActivityType = search.Where<QuerySetting>(u => u.Field == "ActivityType").Select(p => p.Value).SingleOrDefault();
                if (sActivityType != null && sActivityType != "") records = records.Where(u => (int)u.ActivityType== int.Parse(sActivityType));

                sSubject = search.Where<QuerySetting>(u => u.Field == "Subject").Select(p => p.Value).SingleOrDefault();
                if (sSubject != null && sSubject != "") records = records.Where(u => u.Subject.ToLower().Contains(sSubject.ToLower()));

                sStatus = search.Where<QuerySetting>(u => u.Field == "Status").Select(p => p.Value).SingleOrDefault();
                if (sStatus != null && sStatus != "") records = records.Where(u => (int)u.Status == int.Parse(sStatus));

                sActivityOwner = search.Where<QuerySetting>(u => u.Field == "ActivityOwner").Select(p => p.Value).SingleOrDefault();
                if (sActivityOwner != null && sActivityOwner != "") records = records.Where(u => u.ActivityOwner.UserName.ToLower().Contains(sActivityOwner.ToLower()));

                sAttendedAccount = search.Where<QuerySetting>(u => u.Field == "AttendedAccount").Select(p => p.Value).SingleOrDefault();
                if (sAttendedAccount != null && sAttendedAccount != "") records = records.Where(u => u.AttendedAccount.Name.ToLower().Contains(sAttendedAccount.ToLower()));

                sAttendedCustomer = search.Where<QuerySetting>(u => u.Field == "AttendedCustomer").Select(p => p.Value).SingleOrDefault();
                if (sAttendedCustomer != null && sAttendedCustomer != "") records = records.Where(u => u.AttendedCustomer.Name.ToLower().Contains(sAttendedCustomer.ToLower()));

                sStartTime = search.Where<QuerySetting>(u => u.Field == "StartTime").Select(p => p.Value).SingleOrDefault();
                if (sStartTime != null && sStartTime != "")
                {
                    DatetimeRange r = new DatetimeRange(sStartTime);
                    if (r.dBegin!=null) records = records.Where(u => u.StartTime >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.StartTime <= r.dEnd);
                }

                sEndTime = search.Where<QuerySetting>(u => u.Field == "AccountOwner").Select(p => p.Value).SingleOrDefault();
                if (sEndTime != null && sEndTime != "")
                {
                    DatetimeRange r = new DatetimeRange(sEndTime);
                    if (r.dBegin != null) records = records.Where(u => u.EndTime >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.EndTime <= r.dEnd);
                }
            }

            return records;
        }

        public int Update(Activity Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(Activity Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return await _context.SaveChangesAsync();
        }
        public Task<int> AddAsync(Activity activity)
        {
            SetCreatedSignature(activity);
            _context.Entry(activity).State = EntityState.Added;
            return _context.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            Activity activity = Get(id);
            activityEntity.Remove(activity);
            return _context.SaveChangesAsync();
        }
        public Task<IQueryable<Activity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}
