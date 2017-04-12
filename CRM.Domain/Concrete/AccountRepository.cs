using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Domain.Concrete
{
    public class AccountRepository: IAccountRepository
    {
        private EFDbContext context;
        private DbSet<Account> accountEntity;

        public AccountRepository (EFDbContext dbcontext)
        {
            this.context = dbcontext;
            accountEntity = context.Set<Account>();
        }

        public void Add(Account account)
        {
            context.Entry(account).State = EntityState.Added;
            context.SaveChanges();
        }

        public Task<int> AddAsync(Account account)
        {
            context.Entry(account).State = EntityState.Added;
            return context.SaveChangesAsync();
        }

        public IQueryable<Account> GetAll()
        {
            return accountEntity.Include(u=>u.AccountOwner).AsQueryable();
        }

        public Task<IQueryable<Account>> GetAllAsync()
        {
            return null; // delay accountEntity.AsQueryableAsync();
        }

        public IQueryable<Account> GetCodesList(string searchName, int page)
        {
            if (searchName!="")
                return accountEntity.Where(p => p.Name.ToLower().Contains(searchName.ToLower())).Skip(5 * (page-1)).Take(5).AsQueryable();
            else
                return accountEntity.Skip(5 * (page-1)).Take(5).AsQueryable();
        }

        public Account Get(int id)
        {
            return accountEntity.Include(u => u.AccountOwner).SingleOrDefault(s => s.AccountID == id);
        }

        public Task<Account> GetAsync(int id)
        {
            return accountEntity.Include(u => u.AccountOwner).SingleOrDefaultAsync(s => s.AccountID == id);
        }

        public void Delete(int id)
        {
            Account account = Get(id);
            accountEntity.Remove(account);
            context.SaveChanges();
        }

        public Task<int> DeleteAsync(int id)
        {
            Account account = Get(id);
            accountEntity.Remove(account);
            return context.SaveChangesAsync();
        }

        public void Update(Account account)
        {
            context.Update(account);
            context.SaveChanges();
        }

        public Task<int> UpdateAsync(Account account)
        {
            context.Update(account);
            return context.SaveChangesAsync();
        }
    }
}
