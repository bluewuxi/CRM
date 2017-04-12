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

                    b.Property<string>("AccountOwnerId");

                    b.Property<int>("AccountType");

                    b.Property<string>("Address");

                    b.Property<DateTime?>("Birthdate");

                    b.Property<int>("ContactGender");

                    b.Property<string>("ContactName")
                        .HasMaxLength(50);

                    b.Property<string>("EMail")
                        .HasMaxLength(50);

                    b.Property<string>("Mobile")
                        .HasMaxLength(30);

                    b.Property<string>("ModifiedById");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("ShortName")
                        .HasMaxLength(50);

                    b.HasKey("AccountID");

                    b.HasIndex("AccountOwnerId");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Activity", b =>
                {
                    b.Property<int>("ActivityID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActivityTime");

                    b.Property<int?>("AttendAccountAccountID");

                    b.Property<int?>("AttendStudentStudentID");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ActivityID");

                    b.HasIndex("AttendAccountAccountID");

                    b.HasIndex("AttendStudentStudentID");

                    b.ToTable("Activities");
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

            modelBuilder.Entity("CRM.Domain.Entities.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DueDate");

                    b.Property<int?>("EnrollmentAgentAccountID");

                    b.Property<int?>("InstituteAccountID");

                    b.Property<string>("Note");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<int?>("StudentID");

                    b.Property<decimal>("Tuition")
                        .HasColumnType("money");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("EnrollmentAgentAccountID");

                    b.HasIndex("InstituteAccountID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcademicBackground")
                        .HasMaxLength(200);

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<int?>("AgentAccountID");

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("ContactName")
                        .HasMaxLength(50);

                    b.Property<string>("EMail")
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<string>("Mobile")
                        .HasMaxLength(30);

                    b.Property<string>("ModifiedById");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Nationality")
                        .HasMaxLength(20);

                    b.Property<string>("Note");

                    b.Property<string>("PassportNumber")
                        .HasMaxLength(20);

                    b.Property<string>("PreferName")
                        .HasMaxLength(50);

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("StudentOwnerId");

                    b.HasKey("StudentID");

                    b.HasIndex("AgentAccountID");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("StudentOwnerId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CRM.Domain.Entities.VisaApplication", b =>
                {
                    b.Property<int>("VisaApplicationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientNumber")
                        .HasMaxLength(20);

                    b.Property<string>("Documents")
                        .HasMaxLength(50);

                    b.Property<string>("EamilInForm")
                        .HasMaxLength(50);

                    b.Property<DateTime>("ExpiredDate");

                    b.Property<int?>("InstituteAccountID");

                    b.Property<string>("Note")
                        .HasMaxLength(50);

                    b.Property<string>("PassportNumber")
                        .HasMaxLength(20);

                    b.Property<string>("PhysicalAddress")
                        .HasMaxLength(50);

                    b.Property<string>("PostalAddress")
                        .HasMaxLength(50);

                    b.Property<DateTime>("ReceivedDate");

                    b.Property<int?>("StudentID");

                    b.Property<DateTime>("SubmittedDate");

                    b.Property<string>("VisaAppliedType")
                        .HasMaxLength(50);

                    b.HasKey("VisaApplicationID");

                    b.HasIndex("InstituteAccountID");

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

            modelBuilder.Entity("CRM.Domain.Entities.Account", b =>
                {
                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "AccountOwner")
                        .WithMany()
                        .HasForeignKey("AccountOwnerId");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Activity", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Account", "AttendAccount")
                        .WithMany()
                        .HasForeignKey("AttendAccountAccountID");

                    b.HasOne("CRM.Domain.Entities.Student", "AttendStudent")
                        .WithMany("Activities")
                        .HasForeignKey("AttendStudentStudentID");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Enrollment", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Account", "EnrollmentAgent")
                        .WithMany()
                        .HasForeignKey("EnrollmentAgentAccountID");

                    b.HasOne("CRM.Domain.Entities.Account", "Institute")
                        .WithMany()
                        .HasForeignKey("InstituteAccountID");

                    b.HasOne("CRM.Domain.Entities.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Student", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Account", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentAccountID");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("CRM.Domain.Entities.ApplicationUser", "StudentOwner")
                        .WithMany()
                        .HasForeignKey("StudentOwnerId");
                });

            modelBuilder.Entity("CRM.Domain.Entities.VisaApplication", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Account", "Institute")
                        .WithMany()
                        .HasForeignKey("InstituteAccountID");

                    b.HasOne("CRM.Domain.Entities.Student", "Student")
                        .WithMany("VisaApplications")
                        .HasForeignKey("StudentID");
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
        }
    }
}
