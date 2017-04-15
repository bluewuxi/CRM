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
    public class VisaApplicationRepository: BaseRepository, IVisaApplicationRepository
    {
        private EFDbContext context;
        private DbSet<VisaApplication> visaEntity;

        public VisaApplicationRepository(EFDbContext dbcontext)
        {
            this.context = dbcontext;
            visaEntity = context.Set<VisaApplication>();
        }

        public void Add(VisaApplication visa)
        {
            context.Entry(visa).State = EntityState.Added;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public VisaApplication Get(int id)
        {
            return visaEntity.Include(u => u.Student).SingleOrDefault(s => s.VisaApplicationID == id);
        }
        public IQueryable<VisaApplication> GetAll()
        {
            return visaEntity.Include(u => u.Student).AsQueryable();
        }
        public void Update(VisaApplication Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            context.SaveChanges();
        }

        public Task<int> AddAsync(VisaApplication visa)
        {
            context.Entry(visa).State = EntityState.Added;
            SetCreatedSignature(visa);
            return context.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<VisaApplication> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<VisaApplication>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(VisaApplication Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            return context.SaveChangesAsync();
        }
    }
}
