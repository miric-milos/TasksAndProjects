using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models.ViewModels
{
    public class CreateTaskViewModel
    {
        public string Description { get; set; }

        public DateTime Deadline { get; set; }
    }
}
