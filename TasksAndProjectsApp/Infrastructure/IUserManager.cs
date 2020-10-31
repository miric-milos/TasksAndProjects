using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public interface IUserManager
    {
        AppUser GetUser(int userId);
        AppUser GetUser(string userName, string password);
        void CreateUser(AppUser user, string password);
    }
}
