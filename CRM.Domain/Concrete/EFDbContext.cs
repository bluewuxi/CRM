using CRM.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRM.Domain.Concrete
{
    public class EFDbContext: IdentityDbContext<ApplicationUser>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Enrollment>().HasOne(p => p.EnrollmentAgent).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Enrollment>().HasOne(p => p.Institute).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Application>().HasOne(p => p.ApplicationAgent).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Application>().HasOne(p => p.Institute).WithMany().OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<VisaApplication> VisaApplications { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
