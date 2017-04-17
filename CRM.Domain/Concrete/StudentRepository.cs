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
    public class StudentRepository: BaseRepository, IStudentRepository
    {
        private EFDbContext context;
        private DbSet<Student> LeadEntity;

        public StudentRepository(EFDbContext dbcontext)
        {
            this.context = dbcontext;
            LeadEntity = context.Set<Student>();
        }
        public int Add(Student Item)
        {
            context.Entry(Item).State = EntityState.Added;
            SetCreatedSignature(Item);
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Student Get(int id)
        {
            throw new NotImplementedException();
        }
        public IQueryable<Student> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> GetAll()
        {
            throw new NotImplementedException();
        }
        public int Update(Student Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            return context.SaveChanges();
        }

        public Task<int> AddAsync(Student Item)
        {
            context.Entry(Item).State = EntityState.Added;
            SetCreatedSignature(Item);
            return context.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Student> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<Student>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(Student Item)
        {
            throw new NotImplementedException();
        }
}
}
