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
        private readonly IHttpContextAccessor _httpContext;

        public AuthManager(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public AppUser GetAuthenticatedUser()
        {
            int userid = int.Parse(GetCookieValue("userid"));

            AppUser user = AppUser.Users.FirstOrDefault(u => u.Id == userid);

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