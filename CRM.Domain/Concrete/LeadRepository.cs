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
            throw new NotImplementedException();
        }
        public Lead Get(int id)
        {
            return leadEntity.Include(u => u.CustomerOwner).SingleOrDefault(s => s.CustomerID == id);
        }
        public IQueryable<Lead> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            IQueryable<Lead> records= leadEntity.Include(u => u.CustomerOwner).AsQueryable();
            if (search != null && search.Count() > 0)
            {
                string searchName, searchOwner;
                searchName = search.Where<QuerySetting>(u => u.Field == "leadName").Select(p => p.Value).SingleOrDefault();
                if (searchName != null && searchName != "") records = records.Where(u => u.Name.ToLower().Contains(searchName.ToLower()) || u.PreferName.ToLower().Contains(searchName.ToLower()));
                searchOwner = search.Where<QuerySetting>(u => u.Field == "customerOwner").Select(p => p.Value).SingleOrDefault();
                if (searchOwner != null && searchOwner != "") records = records.Where(u => u.CustomerOwner.UserName.ToLower().Contains(searchOwner.ToLower()));
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
            return context.SaveChanges();
        }

        public Task<int> AddAsync(Lead lead)
        {
            context.Entry(lead).State = EntityState.Added;
            SetCreatedSignature(lead);
            return context.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Lead> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<Lead>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(Lead Item)
        {
            throw new NotImplementedException();
        }
    }
}
