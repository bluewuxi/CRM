using CRM.Domain.Concrete;
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
    public interface IBaseRepository
    {
        string UserContext { get; set; }
        void SetCreatedSignature(IEntity item);
        void SetModifiedSignature(IEntity item);
    }

    public interface IRepository<T> : IBaseRepository where T: IEntity, new()
    {
        int Add(T Item);
        int Delete(int id);
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(List<QuerySetting> search, List<QuerySetting> sort);

        int Update(T Item);

        Task<int> AddAsync(T Item);
        Task<int> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        //Task<IQueryable<T>> GetAllAsync();
        Task<int> UpdateAsync(T Item);

        //string UserContext { get; set; }

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
