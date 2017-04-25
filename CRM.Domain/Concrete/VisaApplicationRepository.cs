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
            SetCreatedSignature(visa);
            context.Entry(visa).State = EntityState.Added;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            VisaApplication item = Get(id);
            visaEntity.Remove(item);
            return context.SaveChanges();
        }
        public VisaApplication Get(int id)
        {
            return visaEntity.Include(u => u.Student)
                .Include(i=>i.Institute)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefault(s => s.VisaApplicationID == id);
        }
        public async Task<VisaApplication> GetAsync(int id)
        {
            return await visaEntity.Include(u => u.Student)
                .Include(i => i.Institute)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefaultAsync(s => s.VisaApplicationID == id);
        }
        public IQueryable<VisaApplication> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            string sStudentName, sInstituteName, sStatus, sVisaAppliedType, sVisaType, sSubmittedDate, sExpiredDate;
            IQueryable<VisaApplication> records = visaEntity.Include(u => u.Student).Include(i => i.Institute).AsQueryable();

            if (search != null && search.Count() > 0)
            {
                sStudentName = search.Where<QuerySetting>(u => u.Field == "StudentName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sStudentName != null && sStudentName != "") records = records.Where(u => u.Student.Name.ToLower().Contains(sStudentName.ToLower())
                    || u.Student.PreferName.ToLower().Contains(sStudentName.ToLower()));

                sInstituteName = search.Where<QuerySetting>(u => u.Field == "InstituteName").Select(p => p.Value).SingleOrDefault().Trim();
                if (sInstituteName != null && sInstituteName != "") records = records.Where(u => u.Institute.Name.ToLower().Contains(sInstituteName.ToLower())
                    || u.Institute.ShortName.ToLower().Contains(sInstituteName.ToLower()));

                sVisaAppliedType = search.Where<QuerySetting>(u => u.Field == "VisaAppliedType").Select(p => p.Value).SingleOrDefault();
                if (sVisaAppliedType != null && sVisaAppliedType != "") records = records.Where(u => (int)u.VisaAppliedType == int.Parse(sVisaAppliedType));

                sStatus = search.Where<QuerySetting>(u => u.Field == "Status").Select(p => p.Value).SingleOrDefault();
                if (sStatus != null && sStatus != "") records = records.Where(u => (int)u.Status == int.Parse(sStatus));

                sVisaType = search.Where<QuerySetting>(u => u.Field == "VisaType").Select(p => p.Value).SingleOrDefault().Trim();
                if (sVisaType != null && sVisaType != "") records = records.Where(u => u.VisaType.ToLower().Contains(sVisaType.ToLower()));

                sSubmittedDate = search.Where<QuerySetting>(u => u.Field == "SubmittedDate").Select(p => p.Value).SingleOrDefault();
                if (sSubmittedDate != null && sSubmittedDate != "")
                {
                    DatetimeRange r = new DatetimeRange(sSubmittedDate);
                    if (r.dBegin != null) records = records.Where(u => u.SubmittedDate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.SubmittedDate <= r.dEnd);
                }

                sExpiredDate = search.Where<QuerySetting>(u => u.Field == "ExpiredDate").Select(p => p.Value).SingleOrDefault();
                if (sExpiredDate != null && sExpiredDate != "")
                {
                    DatetimeRange r = new DatetimeRange(sSubmittedDate);
                    if (r.dBegin != null) records = records.Where(u => u.ExpiredDate >= r.dBegin);
                    if (r.dEnd != null) records = records.Where(u => u.ExpiredDate <= r.dEnd);
                }
            }

            return records;
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
        public async Task<int> DeleteAsync(int id)
        {
            VisaApplication item = Get(id);
            visaEntity.Remove(item);
            return await context.SaveChangesAsync();
        }
        public Task<IQueryable<VisaApplication>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateAsync(VisaApplication Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            return await context.SaveChangesAsync();
        }
    }
}
