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
        private DbSet<Student> StudentEntity;

        public StudentRepository(EFDbContext dbcontext)
        {
            this.context = dbcontext;
            StudentEntity = context.Set<Student>();
        }
        public int Add(Student Item)
        {
            context.Entry(Item).State = EntityState.Added;
            SetCreatedSignature(Item);
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            Student account = Get(id);
            StudentEntity.Remove(account);
            return context.SaveChanges();
        }
        public Student Get(int id)
        {
            return StudentEntity.Include(u => u.CustomerOwner).Include(a=>a.Agent).SingleOrDefault(s => s.CustomerID == id);
        }
        public IQueryable<Student> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            IQueryable<Student> records = StudentEntity.Include(u => u.CustomerOwner).Include(a=>a.Agent).AsQueryable();
            if (search != null && search.Count() > 0)
            {
                string searchName, searchOwner;
                searchName = search.Where<QuerySetting>(u => u.Field == "customerName").Select(p => p.Value).SingleOrDefault();
                if (searchName != null && searchName != "") records = records.Where(u => u.Name.ToLower().Contains(searchName.ToLower()) || u.PreferName.ToLower().Contains(searchName.ToLower()));
                searchOwner = search.Where<QuerySetting>(u => u.Field == "customerOwner").Select(p => p.Value).SingleOrDefault();
                if (searchOwner != null && searchOwner != "") records = records.Where(u => u.CustomerOwner.UserName.ToLower().Contains(searchOwner.ToLower()));
            }
            return records;
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
        public async Task<int> DeleteAsync(int id)
        {
            Student account = Get(id);
            StudentEntity.Remove(account);
            return await context.SaveChangesAsync();
        }
        public async Task<Student> GetAsync(int id)
        {
            return await StudentEntity.Include(u => u.CustomerOwner).Include(a => a.Agent).SingleOrDefaultAsync(s => s.CustomerID == id);
        }
        public Task<IQueryable<Student>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateAsync(Student Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            return await context.SaveChangesAsync();
        }
    }
}
