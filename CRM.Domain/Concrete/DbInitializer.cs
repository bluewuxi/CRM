using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;

namespace CRM.Domain.Concrete
{
    public class DbInitializer
    {

        public static async void InitializeAsync(EFDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            AccountRepository accountRep = new AccountRepository(context);
            if (accountRep.GetAll().Any())
            {
                return;   // DB has been seeded
            }

            //Seed roles
            IdentityRole[] roles = new IdentityRole[] {
                new IdentityRole {Name = "Admins"},
                new IdentityRole {Name = "Users"},
                new IdentityRole {Name = "Powerusers"}
            };

            foreach (IdentityRole role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            //Seed users
            var users = new ApplicationUser[]
            {
                new ApplicationUser { UserName = "admin", Email = "admin@foo.com" },
                new ApplicationUser { UserName = "master", Email = "master@foo.com" },
                new ApplicationUser { UserName = "super", Email = "super@foo.com" },
                new ApplicationUser { UserName = "user1", Email = "user1@foo.com" },
                new ApplicationUser { UserName = "user2", Email = "user2@foo.com" },
                new ApplicationUser { UserName = "user3", Email = "user3@foo.com" },
            };

            foreach (ApplicationUser user in users)
            {
                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, "Users");
                if (user.UserName=="admin")
                    await userManager.AddToRoleAsync(user, "Admins");
            }

            ApplicationUser master = await userManager.FindByNameAsync("master");
            ApplicationUser admin = await userManager.FindByNameAsync("admin");

            var students = new Student[]
            {
                new Student{Name="Carson", Rating="0",Gender= GenderEnum.Male,ContactName="Carson", CustomerOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Student{Name="Meredith",Rating="0",Gender= GenderEnum.Male,CustomerOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Student{Name="Arturo",Rating="0",Gender= GenderEnum.Female,CustomerOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Student{Name="Gytis",Rating="0",Gender= GenderEnum.Male, CustomerOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Student{Name="Yan",Rating="0",Gender= GenderEnum.Female,CustomerOwnerID=admin.Id, ModifiedByID=admin.Id,ModifiedTime=DateTime.Now,CreatedByID=admin.Id,CreatedTime=DateTime.Now},
                new Student{Name="Peggy",Rating="0",Gender= GenderEnum.Male, CustomerOwnerID=admin.Id, ModifiedByID=admin.Id,ModifiedTime=DateTime.Now,CreatedByID=admin.Id,CreatedTime=DateTime.Now},
                new Student{Name="Laura",Rating="0",Gender= GenderEnum.Female,CustomerOwnerID=admin.Id, ModifiedByID=admin.Id,ModifiedTime=DateTime.Now,CreatedByID=admin.Id,CreatedTime=DateTime.Now},
                new Student{Name="Nino",Rating="0",Gender= GenderEnum.Male, CustomerOwnerID=admin.Id, ModifiedByID=admin.Id,ModifiedTime=DateTime.Now,CreatedByID=admin.Id,CreatedTime=DateTime.Now}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }

            var accounts = new Account[]
            {
                new Account{Name="The University of Auckland", ShortName="UOA",  AccountType= Account.AccountTypeEnum.Institute, ContactName="Carson",ContactGender= GenderEnum.Male,AccountOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Account{Name="Auckland University of Technique",ShortName="AUT",  AccountType= Account.AccountTypeEnum.Institute,AccountOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Account{Name="East Institue of Technology",ShortName="EIT", AccountType= Account.AccountTypeEnum.Institute,AccountOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Account{Name="Agent of Auckland", ShortName="AOA",AccountType= Account.AccountTypeEnum.Agent, AccountOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Account{Name="Agent of Wellington", ShortName="AOW", AccountType= Account.AccountTypeEnum.Agent, AccountOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Account{Name="Agent of Queen Street",ShortName="AOQ", AccountType= Account.AccountTypeEnum.Agent, AccountOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Account{Name="Travel in NZ",ShortName="TIN", AccountType= Account.AccountTypeEnum.Other, AccountOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now},
                new Account{Name="Air of New Zealand",ShortName="ANZ", AccountType= Account.AccountTypeEnum.Other, AccountOwnerID=master.Id, ModifiedByID=master.Id,ModifiedTime=DateTime.Now,CreatedByID=master.Id,CreatedTime=DateTime.Now}
            };
            foreach (Account s in accounts)
            {
                await accountRep.AddAsync(s);
            }
            await context.SaveChangesAsync();


        }
    }
}
