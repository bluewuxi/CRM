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
            return _entity.Include(u => u.Student)
                .Include(a => a.EnrollmentAgent)
                .Include(i=>i.Institute)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefault(s => s.EnrollmentID == id);
        }
        public async Task<Enrollment> GetAsync(int id)
        {
            return await _entity.Include(u => u.Student)
                .Include(a => a.EnrollmentAgent)
                .Include(i => i.Institute)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefaultAsync(s => s.EnrollmentID == id);
        }
        public IQueryable<Enrollment> GetAll()
        {
            return _entity.Include(u => u.Student).Include(a=>a.EnrollmentAgent).Include(i => i.Institute).AsQueryable();
        }
        public IQueryable<Enrollment> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            string sStudentName, sInstituteName, sAgentName, sStatus, sPaymentDate, sDueDate, sEndDate;

            IQueryable<Enrollment> records= _entity.Include(u => u.Student).Include(a => a.EnrollmentAgent).Include(i => i.Institute).AsQueryable();

            if (search != null && search.Count() > 0)
            {
                sStudentName = search.Where<QuerySetting>(u => u.Field == "StudentName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sStudentName != null && sStudentName != "") records = records.Where(u => u.Student.Name.ToLower().Contains(sStudentName.ToLower())
                    || u.Student.PreferName.ToLower().Contains(sStudentName.ToLower()));

                sInstituteName = search.Where<QuerySetting>(u => u.Field == "InstituteName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sInstituteName != null && sInstituteName != "") records = records.Where(u => u.Institute.Name.ToLower().Contains(sInstituteName.ToLower())
                    || u.Institute.ShortName.ToLower().Contains(sInstituteName.ToLower()));

                sAgentName = search.Where<QuerySetting>(u => u.Field == "AgentName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sAgentName != null && sAgentName != "") records = records.Where(u => u.EnrollmentAgent.Name.ToLower().Contains(sAgentName.ToLower())
                    || u.EnrollmentAgent.ShortName.ToLower().Contains(sAgentName.ToLower()));

                sStatus = search.Where<QuerySetting>(u => u.Field == "Status").Select(p => p.Value).SingleOrDefault();
                if (sStatus != null && sStatus != "") records = records.Where(u => (int)u.Status == int.Parse(sStatus));

                sPaymentDate = search.Where<QuerySetting>(u => u.Field == "sPaymentDate").Select(p => p.Value).SingleOrDefault();
                if (sPaymentDate != null && sPaymentDate != "")
                {
                    DatetimeRange r = new DatetimeRange(sPaymentDate);
                    if (r.dBegin != null) records = records.Where(u => u.PaymentDate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.PaymentDate <= r.dEnd);
                }

                sDueDate = search.Where<QuerySetting>(u => u.Field == "sDueDate").Select(p => p.Value).SingleOrDefault();
                if (sPaymentDate != null && sPaymentDate != "")
                {
                    DatetimeRange r = new DatetimeRange(sDueDate);
                    if (r.dBegin != null) records = records.Where(u => u.DueDate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.DueDate <= r.dEnd);
                }

                sEndDate = search.Where<QuerySetting>(u => u.Field == "sEndDate").Select(p => p.Value).SingleOrDefault();
                if (sPaymentDate != null && sPaymentDate != "")
                {
                    DatetimeRange r = new DatetimeRange(sEndDate);
                    if (r.dBegin != null) records = records.Where(u => u.EndDate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.EndDate <= r.dEnd);
                }
            }
            return records;
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
