using CRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Concrete
{
    class StudentRepository
    {
        void Add(Student Item)
        {
            throw new NotImplementedException();
        }
        void Delete(int id)
        {
            throw new NotImplementedException();
        }
        Student Get(int id)
        {
            throw new NotImplementedException();
        }
        IQueryable<Student> GetAll()
        {
            throw new NotImplementedException();
        }
        void Update(Student Item)
        {
            throw new NotImplementedException();
        }

        Task<int> AddAsync(Student Item)
        {
            throw new NotImplementedException();
        }
        Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        Task<Student> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        Task<IQueryable<Student>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        Task<int> UpdateAsync(Student Item)
        {
            throw new NotImplementedException();
        }
}
}
