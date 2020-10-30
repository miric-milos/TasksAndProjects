using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models
{
    public class Task
    {
        public Status Status { get; set; }

        public int Progress { get; set; }

        public DateTime Deadline { get; set; }

        public string Description { get; set; }


        // relations
        public AppUser Assignee { get; set; }

        public virtual Project Project { get; set; }
    }
}
