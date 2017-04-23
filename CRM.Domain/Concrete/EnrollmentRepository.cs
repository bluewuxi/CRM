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
    public class EnrollmentRepository: BaseRepository, IEnrollmentRepository
    {
        private EFDbContext _context;
        private DbSet<Enrollment> _entity;

        public EnrollmentRepository(EFDbContext dbcontext)
        {
            this._context = dbcontext;
            _entity = _context.Set<Enrollment>();
        }

        public int Add(Enrollment enrollment)
        {
            _context.Entry(enrollment).State = EntityState.Added;
            SetCreatedSignature(enrollment);
            return _context.SaveChanges();
        }
        public int Delete(int id)
        {
            Enrollment item = Get(id);
            _entity.Remove(item);
            return _context.SaveChanges();
        }
        public Enrollment Get(int id)
        {
            return _entity.Include(u => u.Student).Include(a => a.EnrollmentAgent).Include(i=>i.Institute).SingleOrDefault(s => s.EnrollmentID == id);
        }
        public IQueryable<Enrollment> GetAll()
        {
            return _entity.Include(u => u.Student).Include(a=>a.EnrollmentAgent).Include(i => i.Institute).AsQueryable();
        }
        public IQueryable<Enrollment> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            return _entity.Include(u => u.Student).Include(a => a.EnrollmentAgent).Include(i => i.Institute).AsQueryable();
        }
        public int Update(Enrollment Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            return _context.SaveChanges();
        }

        public async Task<int> AddAsync(Enrollment enrollment)
        {
            _context.Entry(enrollment).State = EntityState.Added;
            SetCreatedSignature(enrollment);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(int id)
        {
            Enrollment item = Get(id);
            _entity.Remove(item);
            return await _context.SaveChangesAsync();
        }
        public async Task<Enrollment> GetAsync(int id)
        {
            return await _entity.Include(u => u.Student).Include(a => a.EnrollmentAgent).Include(i => i.Institute).SingleOrDefaultAsync(s => s.EnrollmentID == id);
        }
        public Task<IQueryable<Enrollment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateAsync(Enrollment Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            return await _context.SaveChangesAsync();
        }
    }
}
