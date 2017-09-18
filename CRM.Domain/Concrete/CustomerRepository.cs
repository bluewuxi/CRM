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
    public class CustomerRepository: BaseRepository, ICustomerRepository
    {
        private DbSet<Customer> customerEntity;
        private EFDbContext _context;

        public CustomerRepository(EFDbContext context)
        {
            _context = context;
            customerEntity = _context.Customers;
        }
        public Customer Get(int id)
        {
            return customerEntity.Include(u => u.CustomerOwner)
                .Include(a => a.ModifiedBy)
                .Include(a => a.CreatedBy)
                .SingleOrDefault(s => s.CustomerID == id);
        }
        public IQueryable<Customer> GetAll()
        {
            throw new NotImplementedException();
            //return customerEntity;
        }

        public IQueryable<Customer> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            string sOwner, sStudentName, sBirthdate, sMobile;
            IQueryable<Customer> records = customerEntity.Include(u => u.CustomerOwner).AsQueryable();

            if (search != null && search.Count() > 0)
            {
                sOwner = search.Where(u => u.Field == "Owner").Select(p => p.Value).SingleOrDefault();
                if (sOwner != null && sOwner != "") records = records.Where(u => u.CustomerOwnerID == sOwner || u.CustomerOwnerID == null || u.ModifiedByID == sOwner);

                sStudentName = search.Where(u => u.Field == "StudentName").Select(p => p.Value).SingleOrDefault();
                if (sStudentName != null && sStudentName != "") records = records.Where(u => u.Name.ToLower().Contains(sStudentName.ToLower().Trim())
                    || u.PreferName.ToLower().Contains(sStudentName.ToLower().Trim()));

                sMobile = search.Where(u => u.Field == "Mobile").Select(p => p.Value).SingleOrDefault();
                if (sMobile != null && sMobile != "") records = records.Where(u => u.Mobile.Contains(sMobile.Trim()));

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

        public int Add(Customer Item) { throw new NotImplementedException(); }
        public int Delete(int id) { throw new NotImplementedException(); }
        public int Update(Customer Item) { throw new NotImplementedException();}
        public Task<int> AddAsync(Customer Item) {throw new NotImplementedException();}
        public Task<int> DeleteAsync(int id) { throw new NotImplementedException(); }
        public Task<Customer> GetAsync(int id) { throw new NotImplementedException(); }
        public Task<int> UpdateAsync(Customer Item) { throw new NotImplementedException(); }
    }
}
