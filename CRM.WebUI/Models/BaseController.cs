using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WebUI.Models
{
    public class BaseController: Controller
    {
        private UserManager<ApplicationUser> _userMnger;

        public BaseController()
        {
        }
        public void AttachUserManager(UserManager<ApplicationUser> aUserManager)
        {
            _userMnger = aUserManager;
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            ApplicationUser aUser = await GetCurrentUserAsync();
            return aUser.Id;
        }

        public string GetCurrentUserID()
        {
            if (HttpContext.User == null) return null;
            return _userMnger.GetUserId(HttpContext.User);
        }

        public Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userMnger.GetUserAsync(HttpContext.User);
        }

        protected IQueryable<ApplicationUser> GetUsers()
        {
            return _userMnger.Users;
        }
    }
}
