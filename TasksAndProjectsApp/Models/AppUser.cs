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
        public byte[] Password { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public virtual List<AppTask> Tasks { get; set; } = new List<AppTask>();
    }
}
