﻿using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Concrete
{
    public class ApplicationRepository: BaseRepository, IApplicationRepository
    {
        private EFDbContext context;
        private DbSet<Application> applicationEntity;

        public ApplicationRepository(EFDbContext dbcontext)
        {
            this.context = dbcontext;
            applicationEntity = context.Set<Application>();
        }

        public void Add(Application application)
        {
            context.Entry(application).State = EntityState.Added;
            SetCreatedSignature(application);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Application Get(int id)
        {
            return applicationEntity.Include(u => u.Institute).SingleOrDefault(s => s.ApplicationID == id);
        }
        public IQueryable<Application> GetAll()
        {
            return applicationEntity.Include(u => u.Institute).AsQueryable();
        }
        public void Update(Application Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            context.SaveChanges();
        }

        public Task<int> AddAsync(Application application)
        {
            context.Entry(application).State = EntityState.Added;
            SetCreatedSignature(application);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Application> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<Application>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(Application Item)
        {
            throw new NotImplementedException();
        }
    }
}