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

        public void LogIn(AppUser user, bool isPersistant)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = isPersistant ? DateTime.Now.AddDays(365) : DateTime.Now.AddHours(1);

            SetCookie("userId", user.Id.ToString(), options);
            SetCookie("role", user.Role.ToString(), options);
            SetCookie("userName", user.UserName, options);
        }

        public void LogOut()
        {
            if (IsCookieSet("userid") && IsCookieSet("role") && IsCookieSet("userName"))
            {
                _httpContext.HttpContext.Response.Cookies.Delete("userid");
                _httpContext.HttpContext.Response.Cookies.Delete("role");
                _httpContext.HttpContext.Response.Cookies.Delete("userName");
            }
        }

        public bool UserIsAuthenticated()
        {
            return IsCookieSet("userid") && IsCookieSet("role") && IsCookieSet("userName");
        }

        #region private
        private void SetCookie(string key, string value, CookieOptions options)
        {
            // set cookie
            _httpContext.HttpContext.Response.Cookies.Append(key, value, options);
        }

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