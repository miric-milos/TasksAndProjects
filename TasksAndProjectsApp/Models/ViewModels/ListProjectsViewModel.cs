using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models.ViewModels
{
    public class ListProjectsViewModel
    {
        public Role UserRole { get; set; }

        public List<Project> Projects { get; set; }
    }
}
