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

        public void CreateUser(AppUser user, string password)
        {
            byte[] passwordHash = Hash.HashString(password);

            user.Password = passwordHash;
            _db.Users.Add(user);
            _db.SaveChanges();
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
    }
}
