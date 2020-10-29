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

        public void LogIn(int userId, bool isPersistant)
        {
            CookieOptions options = new CookieOptions();

            options.Expires = isPersistant ? DateTime.Now.AddHours(1) : DateTime.Now.AddDays(365);

            // set cookie
            _httpContext.HttpContext.Response.Cookies.Append("userid", userId.ToString(), options);
        }

        public void LogOut()
        {
            if (IsCookieSet("userid"))
                _httpContext.HttpContext.Response.Cookies.Delete("userid");            
        }

        #region private
        private bool IsCookieSet(string cookieName)
        {
            return !string.IsNullOrEmpty(
                _httpContext.HttpContext.Request.Cookies[cookieName]);
        }
        #endregion
    }
}