using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public interface IProjectManager
    {
        void DeleteProject(int projId);
        Project GetProjectById(int projId);
        void CreateProject(Project proj);
    }
}
