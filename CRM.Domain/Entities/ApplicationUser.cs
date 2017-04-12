using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRM.Domain.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Department { get; set; }
        public bool Disable { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
    }
}
