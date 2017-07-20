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
            return _entity.Include(u => u.Student).Include(a=>a.EnrollmentAgent).Include(i => i.Institute);
        }
        public IQueryable<Enrollment> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            string sStudentName, sInstituteName, sAgentName, sStatus, sPaymentDate, sDueDate, sEndDate, sOwner;

            IQueryable<Enrollment> records= _entity.Include(u => u.Student).Include(a => a.EnrollmentAgent).Include(i => i.Institute).AsQueryable();

            if (search != null && search.Count() > 0)
            {
                sOwner = search.Where(u => u.Field == "Owner").Select(p => p.Value).SingleOrDefault();
                if (sOwner != null && sOwner != "") records = records.Where(u => u.Student.CustomerOwnerID == sOwner || u.Student.ModifiedByID == sOwner || u.Student.CustomerOwnerID == null);

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
        public async Task<int> UpdateAsync(Enrollment Item)
        {
            await CreateActivity(Item);
            SetModifiedSignature(Item);
            _context.Update(Item);
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAsync(Enrollment enrollment)
        {
            await CreateActivity(enrollment);
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

        private async Task<Activity> CreateActivity(Enrollment Item)
        {
            //If it is applied status and SubmittedDate is set or modified, 
            //a activity is automaticly created with StartTime= SubmittedDate + 7

            if (Item.Status != Enrollment.EnrollmentStatusEnum.Actived) return null;

            if (Item.EnrollmentID != 0) // Check if SubmittedDate is changed when it is not a new application
            {
                Enrollment current = await _entity.AsNoTracking()
                        .SingleOrDefaultAsync(x => x.EnrollmentID == Item.EnrollmentID);
                if (current.DueDate == Item.DueDate)
                    return null;
            }

            Activity a = new Activity()
            {
                Status = Activity.ActivityStatusEnum.OpenTask,
                ActivityType = Activity.ActivityTypeEnum.Other,
                Subject = "[System] Beware Due Date of Tuition",
                StartTime = Item.DueDate.AddDays(-30),
                EndTime = Item.DueDate,
                AttendedAccountID = Item.InstituteID,
                AttendedCustomerID = Item.StudentID,
            };
            SetCreatedSignature(a);

            // Set activity owner as the student owner or if student owner is empty, set the current user.
            Student s = await _context.Students.Where(x => x.CustomerID == Item.StudentID).AsNoTracking().SingleOrDefaultAsync();
            a.ActivityOwnerID = (s.CustomerOwnerID == null || s.CustomerOwnerID == "") ? a.ModifiedByID : s.CustomerOwnerID;
            await _context.Activities.AddAsync(a);
            return a;
        }
    }
}
