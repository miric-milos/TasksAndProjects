using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public class ProjectManager : IProjectManager
    {
        // dbcontext dependancy

        public void DeleteProject(int projId)
        {
            var proj = Project.Projects
                .FirstOrDefault(p => p.Id == projId);

            if(proj != null)
            {
                Project.Projects.Remove(proj);
            }
        }

        public Project GetProjectById(int projId)
        {
            return Project.Projects
                .FirstOrDefault(p => p.Id == projId);
        }
    }
}
