using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        // login info
        public string UserName { get; set; }
        public string Password { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Role Role { get; set; }


        public static List<AppUser> Users { get; set; } = new List<AppUser>
        {
            new AppUser{Id=0,FirstName="Milos",LastName="Miric",Role=Role.Administrator ,UserName="miledizna",Password="test123"},
            new AppUser{Id=1,UserName="canekurbla",Password="test123"}
        };
    }
}
