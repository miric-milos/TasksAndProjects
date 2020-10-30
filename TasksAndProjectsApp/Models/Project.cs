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


        // testing purposes
        public static List<Project> Projects { get; set; } = new List<Project>
        {
            new Project{Id=0,Name="Cuphead"},
            new Project{Id=1,Name="Embedded software stuff"}
        };
    }
}
