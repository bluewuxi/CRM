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

        public int Add(VisaApplication visa)
        {
            context.Entry(visa).State = EntityState.Added;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
        public VisaApplication Get(int id)
        {
            return visaEntity.Include(u => u.Student).Include(i=>i.Institute).SingleOrDefault(s => s.VisaApplicationID == id);
        }
        public IQueryable<VisaApplication> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            return visaEntity.Include(u => u.Student).AsQueryable();
        }
        public IQueryable<VisaApplication> GetAll()
        {
            return visaEntity.Include(u => u.Student).Include(i=>i.Institute).AsQueryable();
        }
        public int Update(VisaApplication Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            return context.SaveChanges();
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
        public async Task<VisaApplication> GetAsync(int id)
        {
            return await visaEntity.Include(u => u.Student).Include(i => i.Institute).SingleOrDefaultAsync(s => s.VisaApplicationID == id);
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
