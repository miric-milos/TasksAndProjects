using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using TasksAndProjectsApp.Infrastructure.Security;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public class UserManager : IUserManager
    {
        private readonly DatabaseContext _db;

        public UserManager(DatabaseContext db)
        {
            _db = db;
        }

        public async Task CreateUserAsync(AppUser user, string password)
        {
            byte[] passwordHash = Hash.HashString(password);

            user.Password = passwordHash;
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = GetUser(userId);

            if(user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Unknown error occured!");
            }
        }

        public AppUser GetUser(int userId)
        {
            AppUser user = _db.Users
                .FirstOrDefault(u => u.Id == userId);

            return user;
        }

        public AppUser GetUser(string userName, string password)
        {
            byte[] passwordHash = Hash.HashString(password);

            AppUser user = _db.Users
                .FirstOrDefault(u => u.UserName == userName && u.Password == passwordHash);

            return user;
        }

        public List<AppUser> GetUsers()
        {
            return _db.Users.ToList();
        }

        public IEnumerable<AppUser> GetUsers(Role role)
        {
            var users = _db.Users.Where(u => u.Role == role);
            return users;
        }

        public async Task UpdateUserAsync(AppUser user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
