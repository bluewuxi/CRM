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

        public void Add(Activity activity)
        {
            context.Entry(activity).State = EntityState.Added;
            SetCreatedSignature(activity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
            return activityEntity.Include(a => a.ActivityOwner).Include(a => a.AttendedAccount).Include(a => a.AttendedCustomer).AsQueryable();
        }

        public void Update(Activity Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
        }

        public Task<int> AddAsync(Activity activity)
        {
            SetCreatedSignature(activity);
            context.Entry(activity).State = EntityState.Added;
            return context.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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
