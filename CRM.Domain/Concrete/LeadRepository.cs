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

        public void Add(Lead lead)
        {
            context.Entry(lead).State = EntityState.Added;
            SetCreatedSignature(lead);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Lead Get(int id)
        {
            return leadEntity.Include(u => u.CustomerOwner).SingleOrDefault(s => s.CustomerID == id);
        }
        public IQueryable<Lead> GetAll()
        {
            return leadEntity.Include(u => u.CustomerOwner).AsQueryable();
        }
        public void Update(Lead Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            context.SaveChanges();
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
