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
            Student student = Get(id);
            StudentEntity.Remove(student);
            return context.SaveChanges();
        }
        public Student Get(int id)
        {
            return StudentEntity.Include(u => u.CustomerOwner)
                .Include(a=>a.Agent)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefault(s => s.CustomerID == id);
        }
        public async Task<Student> GetAsync(int id)
        {
            return await StudentEntity.Include(u => u.CustomerOwner)
                .Include(a => a.Agent)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefaultAsync(s => s.CustomerID == id);
        }
        public IQueryable<Student> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            string sStudentName, sPassportNumber, sContactName, sRating, sBirthdate, sMobile;
            IQueryable<Student> records = StudentEntity.Include(u => u.CustomerOwner).Include(a=>a.Agent).AsQueryable();

            if (search != null && search.Count() > 0)
            {
                sStudentName = search.Where(u => u.Field == "StudentName").Select(p => p.Value).SingleOrDefault();
                if (sStudentName != null && sStudentName != "") records = records.Where(u => u.Name.ToLower().Contains(sStudentName.ToLower().Trim())
                    || u.PreferName.ToLower().Contains(sStudentName.ToLower().Trim()));

                sMobile = search.Where(u => u.Field == "Mobile").Select(p => p.Value).SingleOrDefault();
                if (sMobile != null && sMobile != "") records = records.Where(u => u.Mobile.Contains(sMobile.Trim()));

                sContactName = search.Where(u => u.Field == "ContactName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sContactName != null && sContactName != "") records = records.Where(u => u.ContactName.ToLower().Contains(sContactName.ToLower()));

                sPassportNumber = search.Where(u => u.Field == "PassportNumber").Select(p => p.Value).SingleOrDefault();
                if (sPassportNumber != null && sPassportNumber != "") records = records.Where(u => u.PassportNumber.ToLower().Contains(sPassportNumber.ToLower().Trim()));

                sRating = search.Where(u => u.Field == "Rating").Select(p => p.Value).SingleOrDefault();
                if (sRating != null && sRating != "") records = records.Where(u => u.Rating.Equals(sRating.Trim()));

                sBirthdate = search.Where(u => u.Field == "Birthdate").Select(p => p.Value).SingleOrDefault();
                if (sBirthdate != null && sBirthdate != "")
                {
                    DatetimeRange r = new DatetimeRange(sBirthdate);
                    if (r.dBegin != null) records = records.Where(u => u.Birthdate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.Birthdate <= r.dEnd);
                }
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
