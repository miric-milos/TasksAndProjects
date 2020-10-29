using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public interface IAuthManager
    {
        void LogIn(int userId, bool isPersistant);
        void LogOut();
        bool UserIsAuthenticated();
        AppUser GetAuthenticatedUser();
    }
}
