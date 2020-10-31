using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public class AuthManager : IAuthManager
    {
        // if this wasn't needed class would be static 
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserManager _userManager;

        public AuthManager(IHttpContextAccessor httpContext, IUserManager userManager)
        {
            // Inject http context
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public AppUser GetAuthenticatedUser()
        {
            int userId = int.Parse(GetCookieValue("userid"));

            AppUser user = _userManager.GetUser(userId);

            return user;
        }

        public void LogIn(int userId, bool isPersistant)
        {
            CookieOptions options = new CookieOptions();

            options.Expires = isPersistant ? DateTime.Now.AddDays(365) : DateTime.Now.AddHours(1);

            // set cookie
            _httpContext.HttpContext.Response.Cookies.Append("userid", userId.ToString(), options);
        }

        public void LogOut()
        {
            if (IsCookieSet("userid"))
                _httpContext.HttpContext.Response.Cookies.Delete("userid");            
        }

        public bool UserIsAuthenticated()
        {
            return IsCookieSet("userid");
        }

        #region private
        private bool IsCookieSet(string cookieName)
        {
            return !string.IsNullOrEmpty(
                _httpContext.HttpContext.Request.Cookies[cookieName]);
        }

        private string GetCookieValue(string cookieName)
        {
            if (IsCookieSet(cookieName))
            {
                return _httpContext.HttpContext.Request.Cookies[cookieName];
            }

            throw new Exception("Cookie not set!");
        }
        #endregion
    }
}