using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Concrete
{
    public class CustomerRepository: BaseRepository, ICustomerRepository
    {
        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }
        public IQueryable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
