using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public interface IProjectManager
    {
        Task DeleteProjectAsync(int projId);
        Project GetProject(int projId);
        Task CreateProjectAsync(Project proj);
        List<Project> GetProjects();
        Task UpdateProjectAsync(Project proj);
        Task AssignTaskToProjectAsync(int projId, AppTask task);
    }
}
