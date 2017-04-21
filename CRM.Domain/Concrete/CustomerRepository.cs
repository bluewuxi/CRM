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
            throw new NotImplementedException();
        }
        public IQueryable<Customer> GetAll()
        {
            return customerEntity;
        }
        public IQueryable<Customer> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            return customerEntity;
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
