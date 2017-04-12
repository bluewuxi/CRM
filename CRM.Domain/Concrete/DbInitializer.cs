using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using System;
using System.Linq;

namespace CRM.Domain.Concrete
{
    public class DbInitializer
    {

        public static void Initialize(EFDbContext context)
        {
            AccountRepository accountRep = new AccountRepository(context);
            if (accountRep.GetAll().Any())
            {
                return;   // DB has been seeded
            }

            var accounts = new Account[]
            {
            new Account{Name="Carson", ContactName="Carson",ContactGender= GenderEnum.Male, RegisterDate=DateTime.Parse("2005-09-01")},
            new Account{Name="Meredith",ContactGender= GenderEnum.Female, RegisterDate=DateTime.Parse("2002-09-01")},
            new Account{Name="Arturo",RegisterDate=DateTime.Parse("2003-09-01")},
            new Account{Name="Gytis",RegisterDate=DateTime.Parse("2002-09-01")},
            new Account{Name="Yan",RegisterDate=DateTime.Parse("2002-09-01")},
            new Account{Name="Peggy",RegisterDate=DateTime.Parse("2001-09-01")},
            new Account{Name="Laura",RegisterDate=DateTime.Parse("2003-09-01")},
            new Account{Name="Nino",RegisterDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Account s in accounts)
            {
                accountRep.Add(s);
            }
        }
    }
}
