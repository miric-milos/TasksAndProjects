using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public int Progress { get; set; }

        public DateTime Deadline { get; set; }


        public Task()
        {
            Progress = 0;
            Status = Status.New;
        }

        // relations
        public virtual AppUser Assignee { get; set; }

        public virtual Project Project { get; set; }
    }
}
