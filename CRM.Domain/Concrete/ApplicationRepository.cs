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
            return _entity.Include(u => u.Institute).Include(s=>s.Student)
                .Include(a=>a.ApplicationAgent)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefault(s => s.ApplicationID == id);
        }
        public async Task<Application> GetAsync(int id)
        {
            return await _entity.Include(u => u.Institute)
                .Include(s => s.Student)
                .Include(a => a.ApplicationAgent)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefaultAsync(s => s.ApplicationID == id);
        }

        public IQueryable<Application> GetAll()
        {
            return _entity.Include(u => u.Institute).Include(s => s.Student).Include(a => a.ApplicationAgent).AsQueryable();
        }

        public IQueryable<Application> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            string sStudentName, sInstituteName, sAgentName, sStatus, sSubmittedDate;

            IQueryable<Application> records = _entity.Include(u => u.Institute).Include(s => s.Student).Include(a => a.ApplicationAgent);

            if (search != null && search.Count() > 0)
            {
                sStudentName = search.Where<QuerySetting>(u => u.Field == "StudentName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sStudentName != null && sStudentName != "") records = records.Where(u => u.Student.Name.ToLower().Contains(sStudentName.ToLower())
                    || u.Student.PreferName.ToLower().Contains(sStudentName.ToLower()));

                sInstituteName = search.Where<QuerySetting>(u => u.Field == "InstituteName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sInstituteName != null && sInstituteName != "") records = records.Where(u => u.Institute.Name.ToLower().Contains(sInstituteName.ToLower())
                    || u.Institute.ShortName.ToLower().Contains(sInstituteName.ToLower()));

                sAgentName = search.Where<QuerySetting>(u => u.Field == "AgentName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sAgentName != null && sAgentName != "") records = records.Where(u => u.ApplicationAgent.Name.ToLower().Contains(sAgentName.ToLower())
                    || u.ApplicationAgent.ShortName.ToLower().Contains(sAgentName.ToLower()));

                sStatus = search.Where<QuerySetting>(u => u.Field == "Status").Select(p => p.Value).SingleOrDefault();
                if (sStatus != null && sStatus != "") records = records.Where(u => (int)u.Status == int.Parse(sStatus));

                sSubmittedDate = search.Where<QuerySetting>(u => u.Field == "SubmittedDate").Select(p => p.Value).SingleOrDefault();
                if (sSubmittedDate != null && sSubmittedDate != "")
                {
                    DatetimeRange r = new DatetimeRange(sSubmittedDate);
                    if (r.dBegin != null) records = records.Where(u => u.SubmittedDate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.SubmittedDate <= r.dEnd);
                }
            }

            return records;
        }

        public int Update(Application Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return _context.SaveChanges();
        }
        public async Task<int> UpdateAsync(Application Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return await _context.SaveChangesAsync();
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
        public Task<IQueryable<Application>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
