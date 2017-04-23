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
    public class ApplicationRepository: BaseRepository, IApplicationRepository
    {
        private EFDbContext _context;
        private DbSet<Application> _entity;

        public ApplicationRepository(EFDbContext dbcontext)
        {
            this._context = dbcontext;
            _entity = _context.Set<Application>();
        }

        public int Add(Application application)
        {
            _context.Entry(application).State = EntityState.Added;
            SetCreatedSignature(application);
            return _context.SaveChanges();
        }
        public int Delete(int id)
        {
            Application item = Get(id);
            _entity.Remove(item);
            return _context.SaveChanges();
        }
        public Application Get(int id)
        {
            return _entity.Include(u => u.Institute).Include(s=>s.Student).Include(a=>a.ApplicationAgent).SingleOrDefault(s => s.ApplicationID == id);
        }

        public IQueryable<Application> GetAll()
        {
            return _entity.Include(u => u.Institute).Include(s => s.Student).Include(a => a.ApplicationAgent).AsQueryable();
        }
        public IQueryable<Application> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            return _entity.Include(u => u.Institute).Include(s => s.Student).Include(a => a.ApplicationAgent).AsQueryable();
        }

        public int Update(Application Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            return _context.SaveChanges();
        }

        public Task<int> AddAsync(Application application)
        {
            _context.Entry(application).State = EntityState.Added;
            SetCreatedSignature(application);
            return _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Application item = Get(id);
            _entity.Remove(item);
            return await _context.SaveChangesAsync();
        }
        public async Task<Application> GetAsync(int id)
        {
            return await _entity.Include(u => u.Institute).Include(s => s.Student).Include(a => a.ApplicationAgent).SingleOrDefaultAsync(s => s.ApplicationID == id);
        }
        public Task<IQueryable<Application>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateAsync(Application Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            return await _context.SaveChangesAsync();
        }
    }
}
