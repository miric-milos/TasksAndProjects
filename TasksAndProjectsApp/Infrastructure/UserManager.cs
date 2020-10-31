using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
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

        public AppUser GetUserByLoginInfo(string userName, string password)
        {
            string pass = password.GetHashCode().ToString();
            var user = _db.Users
                .FirstOrDefault(u => u.Id == 7);

            return user;
        }
    }
}
