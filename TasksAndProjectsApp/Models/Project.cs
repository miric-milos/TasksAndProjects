using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models
{
    public class Project
    {
        public int Id { get; set; } // 'Code'
        public string Name { get; set; }


        // relations
        public virtual List<AppTask> Tasks { get; set; }
    }
}
