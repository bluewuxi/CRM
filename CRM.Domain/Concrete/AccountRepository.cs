﻿using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Domain.Concrete
{
    public class AccountRepository: BaseRepository, IAccountRepository
    {
        private EFDbContext _context;
        private DbSet<Account> accountEntity;

        public AccountRepository (EFDbContext dbcontext)
        {
            this._context = dbcontext;
            accountEntity = _context.Set<Account>();
        }

        public int Add(Account account)
        {
            SetCreatedSignature(account);
            _context.Entry(account).State = EntityState.Added;
            return _context.SaveChanges();
        }

        public Task<int> AddAsync(Account account)
        {
            _context.Entry(account).State = EntityState.Added;
            SetCreatedSignature(account);
            return _context.SaveChangesAsync();
        }

        public IQueryable<Account> GetAll()
        {
            return accountEntity.Include(u=>u.AccountOwner).AsQueryable();
        }

        public IQueryable<Account> GetAll(List<QuerySetting> search, List<QuerySetting> sort)
        {
            IQueryable<Account> records=accountEntity.Include(u => u.AccountOwner);
            if (search != null && search.Count() > 0)
            {
                string searchName, searchType, searchOwner;
                searchName = search.Where<QuerySetting>(u => u.Field == "AccountName").Select(p => p.Value).SingleOrDefault();
                if (searchName != null && searchName != "") records = records.Where(u => u.Name.ToLower().Contains(searchName.ToLower()));
                searchType = search.Where<QuerySetting>(u => u.Field == "AccountType").Select(p => p.Value).SingleOrDefault();

                //if (searchType != null && searchType != "") records = records.Where(u => u.AccountType== aEnum);
                if (searchType != null && searchType != "") records = records.Where(u => (int)u.AccountType == int.Parse(searchType));
                searchOwner = search.Where<QuerySetting>(u => u.Field == "AccountOwner").Select(p => p.Value).SingleOrDefault();
                if (searchOwner != null && searchOwner != "") records = records.Where(u => u.AccountOwner.UserName.ToLower().Contains(searchOwner.ToLower()));
            }
            return records;
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
            return accountEntity.Include(u => u.AccountOwner).Include(u=>u.ModifiedBy).Include(u=>u.CreatedBy).SingleOrDefault(s => s.AccountID == id);
        }

        public Task<Account> GetAsync(int id)
        {
            return accountEntity.Include(u => u.AccountOwner).Include(u => u.ModifiedBy).Include(u => u.CreatedBy).SingleOrDefaultAsync(s => s.AccountID == id);
        }

        public int Delete(int id)
        {
            Account account = Get(id);
            accountEntity.Remove(account);
            return _context.SaveChanges();
        }

        public Task<int> DeleteAsync(int id)
        {
            Account account = Get(id);
            accountEntity.Remove(account);
            return _context.SaveChangesAsync();
        }

        public int Update(Account Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return _context.SaveChanges();
        }

        public Task<int> UpdateAsync(Account Item)
        {
            SetModifiedSignature(Item);
            _context.Update(Item);
            _context.Entry(Item).Property(x => x.CreatedByID).IsModified = false;
            _context.Entry(Item).Property(x => x.CreatedTime).IsModified = false;
            return _context.SaveChangesAsync();
        }
    }
}
