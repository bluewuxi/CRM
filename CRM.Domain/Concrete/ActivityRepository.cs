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
        private EFDbContext context;
        private DbSet<Activity> activityEntity;

        public ActivityRepository(EFDbContext dbcontext)
        {
            this.context = dbcontext;
            activityEntity = context.Set<Activity>();
        }

        public int Add(Activity activity)
        {
            context.Entry(activity).State = EntityState.Added;
            SetCreatedSignature(activity);
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            Activity activity = Get(id);
            activityEntity.Remove(activity);
            return context.SaveChanges();
        }

        public Activity Get(int id)
        {
            //return activityEntity.Include(u => u.AttendedCustomer).Include(u=>u.AttendedAccount).SingleOrDefault(s => s.ActivityID == id);
            return activityEntity
                .Include(a => a.ActivityOwner)
                .Include(a => a.AttendedAccount)
                .Include(a => a.AttendedCustomer)
                .SingleOrDefault(m => m.ActivityID == id);
        }

        public IQueryable<Activity> GetAll()
        {
            return activityEntity.Include(a => a.ActivityOwner).Include(a => a.AttendedAccount).Include(a => a.AttendedCustomer);
        }

        public IQueryable<Activity> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            return activityEntity.Include(a => a.ActivityOwner).Include(a => a.AttendedAccount).Include("AttendedCustomer");
            //return activityEntity;
        }

        public int Update(Activity Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            return context.SaveChanges();
        }

        public Task<int> AddAsync(Activity activity)
        {
            SetCreatedSignature(activity);
            context.Entry(activity).State = EntityState.Added;
            return context.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            Activity activity = Get(id);
            activityEntity.Remove(activity);
            return context.SaveChangesAsync();
        }
        public Task<Activity> GetAsync(int id)
        {
            return activityEntity
                .Include(a => a.ActivityOwner)
                .Include(a => a.AttendedAccount)
                .Include(a => a.AttendedCustomer)
                .SingleOrDefaultAsync(m => m.ActivityID == id);
        }
        public Task<IQueryable<Activity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(Activity Item)
        {
            SetModifiedSignature(Item);

            context.Update(Item);
            return await context.SaveChangesAsync();
        }
    }
}
