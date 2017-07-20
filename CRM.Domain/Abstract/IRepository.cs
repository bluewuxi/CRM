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
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(List<QuerySetting> search, List<QuerySetting> sort);

        Task<int> AddAsync(T Item);
        Task<int> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<int> UpdateAsync(T Item);

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
    public interface ICustomerRepository : IRepository<Customer>
    {
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
