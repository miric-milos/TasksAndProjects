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
        Task CreateUserAsync(AppUser user, string password);
        List<AppUser> GetUsers();
        IEnumerable<AppUser> GetUsers(Role role);
        Task UpdateUserAsync(AppUser user);
        Task DeleteUserAsync(int userId);
    }
}
