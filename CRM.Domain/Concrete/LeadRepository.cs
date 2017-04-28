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
        private EFDbContext context;
        private DbSet<Lead> leadEntity;

        public LeadRepository(EFDbContext dbcontext)
        {
            this.context = dbcontext;
            leadEntity = context.Set<Lead>();
        }

        public int Add(Lead lead)
        {
            context.Entry(lead).State = EntityState.Added;
            SetCreatedSignature(lead);
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            Lead lead = Get(id);
            leadEntity.Remove(lead);
            return context.SaveChanges();
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
            string sLeadName, sMobile, sSource, sBirthdate;
            IQueryable<Lead> records= leadEntity.Include(u => u.CustomerOwner).AsQueryable();
            if (search != null && search.Count() > 0)
            {
                sLeadName = search.Where<QuerySetting>(u => u.Field == "LeadName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sLeadName != null && sLeadName != "") records = records.Where(u => u.Name.ToLower().Contains(sLeadName.ToLower())
                    || u.PreferName.ToLower().Contains(sLeadName.ToLower()));

                sMobile = search.Where<QuerySetting>(u => u.Field == "Mobile").Select(p => p.Value).SingleOrDefault().Trim();
                if (sMobile != null && sMobile != "") records = records.Where(u => u.Mobile.Contains(sMobile));

                sSource = search.Where<QuerySetting>(u => u.Field == "Source").Select(p => p.Value).SingleOrDefault().Trim();
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
        public int Update(Lead Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return context.SaveChanges();
        }

        public async Task<int> UpdateAsync(Lead Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return await context.SaveChangesAsync();
        }
        public Task<int> AddAsync(Lead lead)
        {
            context.Entry(lead).State = EntityState.Added;
            SetCreatedSignature(lead);
            return context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(int id)
        {
            Lead lead = Get(id);
            leadEntity.Remove(lead);
            return await context.SaveChangesAsync();
        }
        public Task<IQueryable<Lead>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
