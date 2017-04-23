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

        public BaseController(UserManager<ApplicationUser> aUserManager)
        {
            _userMnger = aUserManager;
        }
        public void AttachUserManager(UserManager<ApplicationUser> aUserManager)
        {
            _userMnger = aUserManager;
        }

        public async Task<string> GetUserContextAsync()
        {
            ApplicationUser aUser = await GetCurrentUserAsync();
            if (aUser != null)
                return aUser.Id;
            else
                return "";
        }

        public int BindUserContext(IBaseRepository iRepo)
        {
            if (HttpContext.User == null) return -1;
            iRepo.UserContext = HttpContext.User.Identity.Name;
            //iRepo.UserContext = _userMnger.GetUserId(HttpContext.User);
            return 0;
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
