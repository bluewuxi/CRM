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
    public class EnrollmentRepository: BaseRepository, IEnrollmentRepository
    {
        private EFDbContext context;
        private DbSet<Enrollment> enrollmentEntity;

        public EnrollmentRepository(EFDbContext dbcontext)
        {
            this.context = dbcontext;
            enrollmentEntity = context.Set<Enrollment>();
        }

        public void Add(Enrollment enrollment)
        {
            context.Entry(enrollment).State = EntityState.Added;
            SetCreatedSignature(enrollment);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Enrollment Get(int id)
        {
            return enrollmentEntity.Include(u => u.Student).SingleOrDefault(s => s.EnrollmentID == id);
        }
        public IQueryable<Enrollment> GetAll()
        {
            return enrollmentEntity.Include(u => u.Student).AsQueryable();
        }
        public void Update(Enrollment Item)
        {
            SetModifiedSignature(Item);
            context.Update(Item);
            context.SaveChanges();
        }

        public Task<int> AddAsync(Enrollment enrollment)
        {
            context.Entry(enrollment).State = EntityState.Added;
            SetCreatedSignature(enrollment);
            return context.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Enrollment> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<Enrollment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(Enrollment Item)
        {
            throw new NotImplementedException();
        }
    }
}