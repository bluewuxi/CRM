using CRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Abstract
{
    public enum GenderEnum
    {
        Male=1, Female=2
    }

    public interface IRepository<T> where T: IEntity, new()
    {
        void Add(T Item);
        void Delete(int id);
        T Get(int id);
        IQueryable<T> GetAll();
        void Update(T Item);

        Task<int> AddAsync(T Item);
        Task<int> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        //Task<IQueryable<T>> GetAllAsync();
        Task<int> UpdateAsync(T Item);

        void AttachUserContext(string userid);

    }

    public interface IAccountRepository : IRepository<Account>
    {
    }


    public interface IStudentRepository : IRepository<Student>
    {
    }
    public interface ILeadRepository : IRepository<Lead>
    {
    }
    public interface ICustomerRepository 
    {
        Customer Get(int id);
        IQueryable<Customer> GetAll();
    }

    public interface IActivityRepository : IRepository<Activity>
    {
    }
    public interface IApplicationRepository : IRepository<Application>
    {
    }
    public interface IVisaApplicationRepository : IRepository<VisaApplication>
    {
    }
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
    }

}
