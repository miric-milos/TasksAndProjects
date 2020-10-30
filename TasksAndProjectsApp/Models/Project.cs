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
            new Project{Id=1,Name="Cuphead", Tasks = new List<Task>
            {
                new Task{Id=0, Description = "Render some graphics", Assignee = new AppUser{FirstName="Mile", LastName = "Kitic" }, Progress = 50, Status = Status.InProgress},
                new Task{Id=1, Description = "Create a 2d platform", Assignee = new AppUser{FirstName="Dusan", LastName = "Spasojevic" }, Progress = 10, Status = Status.New}
            } },
            new Project{Id=0,Name="Embedded software stuff", Tasks = new List<Task>()}
        };

        // relations
        public virtual List<Task> Tasks { get; set; }
    }
}
