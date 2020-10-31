using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models
{
    [Table("Tasks")]
    public class AppTask
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public int Progress { get; set; }

        public DateTime Deadline { get; set; }


        public AppTask()
        {
            Progress = 0;
            Status = Status.New;
        }

        // relations
        public virtual AppUser Assignee { get; set; }

        public virtual Project Project { get; set; }
    }
}
