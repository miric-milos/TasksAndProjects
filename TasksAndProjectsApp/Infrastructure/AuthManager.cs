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

        public void LogIn(AppUser user)
        {
            
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
