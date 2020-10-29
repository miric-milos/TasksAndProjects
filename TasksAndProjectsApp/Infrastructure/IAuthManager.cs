using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public interface IAuthManager
    {
        void LogIn(AppUser user);

        void LogOut();
    }
}
