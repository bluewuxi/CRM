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
    public class LeadRepository: BaseRepository, ILeadRepository
    {
        private EFDbContext _context;
        private DbSet<Lead> leadEntity;

        public LeadRepository(EFDbContext dbcontext)
        {
            this._context = dbcontext;
            leadEntity = _context.Set<Lead>();
        }

        public Lead Get(int id)
        {
            return leadEntity.Include(u => u.CustomerOwner)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefault(s => s.CustomerID == id);
        }
        public async Task<Lead> GetAsync(int id)
        {
            return await leadEntity.Include(u => u.CustomerOwner)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefaultAsync(s => s.CustomerID == id);
        }
        public IQueryable<Lead> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            string sOwner, sLeadName, sMobile, sSource, sBirthdate;
            IQueryable<Lead> records= leadEntity.Include(u => u.CustomerOwner).AsQueryable();
            if (search != null && search.Count() > 0)
            {
                sOwner = search.Where(u => u.Field == "Owner").Select(p => p.Value).SingleOrDefault();
                if (sOwner != null && sOwner != "") records = records.Where(u => u.CustomerOwnerID == sOwner || u.ModifiedByID == sOwner || u.CustomerOwnerID == null);

                sLeadName = search.Where<QuerySetting>(u => u.Field == "LeadName").Select(p => p.Value).SingleOrDefault();
                if (sLeadName != null && sLeadName != "") records = records.Where(u => u.Name.ToLower().Contains(sLeadName.ToLower())
                    || u.PreferName.ToLower().Contains(sLeadName.ToLower()));

                sMobile = search.Where<QuerySetting>(u => u.Field == "Mobile").Select(p => p.Value).SingleOrDefault();
                if (sMobile != null && sMobile != "") records = records.Where(u => u.Mobile.Contains(sMobile));

                sSource = search.Where<QuerySetting>(u => u.Field == "Source").Select(p => p.Value).SingleOrDefault();
                if (sSource != null && sSource != "") records = records.Where(u => u.Source.ToLower().Contains(sSource.ToLower()));

                sBirthdate = search.Where<QuerySetting>(u => u.Field == "Birthdate").Select(p => p.Value).SingleOrDefault();
                if (sBirthdate != null && sBirthdate != "")
                {
                    DatetimeRange r = new DatetimeRange(sBirthdate);
                    if (r.dBegin != null) records = records.Where(u => u.Birthdate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.Birthdate <= r.dEnd);
                }
            }
            return records;
        }
        public IQueryable<Lead> GetAll()
        {
            return leadEntity.Include(u => u.CustomerOwner).AsQueryable();
        }

        public async Task<int> UpdateAsync(Lead Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return await _context.SaveChangesAsync();
        }
        public Task<int> AddAsync(Lead lead)
        {
            _context.Entry(lead).State = EntityState.Added;
            SetCreatedSignature(lead);
            return _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(int id)
        {
            Lead lead = Get(id);
            leadEntity.Remove(lead);
            IQueryable<Activity> activities = _context.Activities.Where(a => a.AttendedCustomerID == id);
            foreach (Activity anActivity in activities)
            {
                _context.Remove(anActivity);
            }
            return await _context.SaveChangesAsync();
        }
    }
}
