using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CRM.Domain.Concrete;
using CRM.Domain.Entities;
using CRM.Domain.Abstract;

namespace CRM.WebUI.Data
{
    [DbContext(typeof(EFDbContext))]
    partial class EFDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRM.Domain.Entities.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountOwnerID");

                    b.Property<int>("AccountType");

                    b.Property<string>("Address");

                    b.Property<DateTime?>("Birthdate");

                    b.Property<int>("ContactGender");

                    b.Property<string>("ContactName")
                        .HasMaxLength(50);

                    b.Property<string>("CreatedByID");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("EMail")
                        .HasMaxLength(50);

                    b.Property<string>("Mobile")
                        .HasMaxLength(30);

                    b.Property<string>("ModifiedByID");

                    b.Property<DateTime>("ModifiedTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("ShortName")
                        .HasMaxLength(50);

                    b.HasKey("AccountID");

                    b.HasIndex("AccountOwnerID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ModifiedByID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Activity", b =>
                {
                    b.Property<int>("ActivityID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityOwnerID");

                    b.Property<int>("ActivityType");

                    b.Property<int?>("AttendedAccountID");

                    b.Property<int?>("AttendedCustomerID");

                    b.Property<string>("Content");

                    b.Property<string>("CreatedByID");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("ModifiedByID");

                    b.Property<DateTime>("ModifiedTime");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ActivityID");

                    b.HasIndex("ActivityOwnerID");

                    b.HasIndex("AttendedAccountID");

                    b.HasIndex("AttendedCustomerID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ModifiedByID");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Application", b =>
                {
                    b.Property<int>("ApplicationID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicationAgentID");

                    b.Property<string>("CreatedByID");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("InstituteID");

                    b.Property<string>("ModifiedByID");

                    b.Property<DateTime>("ModifiedTime");

                    b.Property<string>("Note");

                    b.Property<int>("Status");

                    b.Property<int>("StudentID");

                    b.Property<DateTime?>("SubmittedDate");

                    b.Property<decimal>("Tuition")
                        .HasColumnType("money");

                    b.HasKey("ApplicationID");

                    b.HasIndex("ApplicationAgentID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("InstituteID");

                    b.HasIndex("ModifiedByID");

                    b.HasIndex("StudentID");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("CRM.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Department");

                    b.Property<bool>("Disable");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcademicBackground")
                        .HasMaxLength(200);

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("Birthdate");

                    b.Property<string>("CreatedByID");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("CustomerOwnerID");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("EMail")
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<string>("Mobile")
                        .HasMaxLength(30);

                    b.Property<string>("ModifiedByID");

                    b.Property<DateTime>("ModifiedTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note");

                    b.Property<string>("PreferName")
                        .HasMaxLength(50);

                    b.HasKey("CustomerID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("CustomerOwnerID");

                    b.HasIndex("ModifiedByID");

                    b.ToTable("Customers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Customer");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedByID");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<DateTime?>("DueDate");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int?>("EnrollmentAgentID");

                    b.Property<int>("InstituteID");

                    b.Property<string>("ModifiedByID");

                    b.Property<DateTime>("ModifiedTime");

                    b.Property<string>("Note");

                    b.Property<DateTime?>("PaymentDate");

                    b.Property<int>("Status");

                    b.Property<int>("StudentID");

                    b.Property<decimal>("Tuition")
                        .HasColumnType("money");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("EnrollmentAgentID");

                    b.HasIndex("InstituteID");

                    b.HasIndex("ModifiedByID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("CRM.Domain.Entities.VisaApplication", b =>
                {
                    b.Property<int>("VisaApplicationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedByID");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Documents")
                        .HasMaxLength(50);

                    b.Property<string>("EamilInForm")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ExpiredDate");

                    b.Property<int>("InstituteID");

                    b.Property<string>("ModifiedByID");

                    b.Property<DateTime>("ModifiedTime");

                    b.Property<string>("Note")
                        .HasMaxLength(50);

                    b.Property<string>("PassportNumber")
                        .HasMaxLength(20);

                    b.Property<string>("PhysicalAddress")
                        .HasMaxLength(50);

                    b.Property<string>("PostalAddress")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ReceivedDate");

                    b.Property<int>("Status");

                    b.Property<int>("StudentID");

                    b.Property<DateTime?>("SubmittedDate");

                    b.Property<int>("VisaAppliedType");

                    b.Property<string>("VisaType")
                        .HasMaxLength(20);

                    b.HasKey("VisaApplicationID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("InstituteID");

                    b.HasIndex("ModifiedByID");

                    b.HasIndex("StudentID");

                    b.ToTable("VisaApplications");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Lead", b =>
                {
                    b.HasBaseType("CRM.Domain.Entities.Customer");

                    b.Property<string>("Source")
                        .HasMaxLength(50);

                    b.ToTable("Lead");

                    b.HasDiscriminator().HasValue("Lead");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Student", b =>
                {
                    b.HasBaseType("CRM.Domain.Entities.Customer");

                    b.Property<int?>("AgentID");

                    b.Property<string>("ClientNumber")
                        .HasMaxLength(20);

                    b.Property<string>("ContactName")
                        .HasMaxLength(50);

                    b.Property<string>("Nationality")
                        .HasMaxLength(20);

                    b.Property<string>("PassportNumber")
                        .HasMaxLength(20);

                    b.Property<string>("Rating")
                        .HasMaxLength(20);

                    b.HasIndex("AgentID");

                    b.ToTable("Student");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Account", b =>
                {
                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "AccountOwner")
                        .WithMany()
                        .HasForeignKey("AccountOwnerID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Activity", b =>
                {
                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ActivityOwner")
                        .WithMany()
                        .HasForeignKey("ActivityOwnerID");

                    b.HasOne("CRM.Domain.Entities.Account", "AttendedAccount")
                        .WithMany()
                        .HasForeignKey("AttendedAccountID");

                    b.HasOne("CRM.Domain.Entities.Customer", "AttendedCustomer")
                        .WithMany("Activities")
                        .HasForeignKey("AttendedCustomerID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Application", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Account", "ApplicationAgent")
                        .WithMany()
                        .HasForeignKey("ApplicationAgentID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("CRM.Domain.Entities.Account", "Institute")
                        .WithMany()
                        .HasForeignKey("InstituteID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");

                    b.HasOne("CRM.Domain.Entities.Student", "Student")
                        .WithMany("Applications")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Domain.Entities.Customer", b =>
                {
                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "CustomerOwner")
                        .WithMany()
                        .HasForeignKey("CustomerOwnerID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Enrollment", b =>
                {
                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("CRM.Domain.Entities.Account", "EnrollmentAgent")
                        .WithMany()
                        .HasForeignKey("EnrollmentAgentID");

                    b.HasOne("CRM.Domain.Entities.Account", "Institute")
                        .WithMany()
                        .HasForeignKey("InstituteID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");

                    b.HasOne("CRM.Domain.Entities.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Domain.Entities.VisaApplication", b =>
                {
                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("CRM.Domain.Entities.Account", "Institute")
                        .WithMany()
                        .HasForeignKey("InstituteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");

                    b.HasOne("CRM.Domain.Entities.Student", "Student")
                        .WithMany("VisaApplications")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CRM.Domain.Entities.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CRM.Domain.Entities.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Domain.Entities.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRM.Domain.Entities.Student", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Account", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID");
                });
        }
    }
}
