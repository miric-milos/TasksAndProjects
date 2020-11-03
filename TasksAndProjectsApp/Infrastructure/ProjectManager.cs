using Microsoft.EntityFrameworkCore;
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
        private readonly DatabaseContext _db;

        public ProjectManager(DatabaseContext db)
        {
            _db = db;
        }

        public async Task DeleteProjectAsync(int projId)
        {
            Project proj = GetProject(projId);

            if(proj == null)
            {
                throw new Exception("Unknown error occured!");
            }

            _db.Projects.Remove(proj);
            await _db.SaveChangesAsync();
        }

        public Project GetProject(int projId)
        {
            return _db.Projects
                .Include(p => p.Tasks)
                .ThenInclude(t => t.Assignee)
                .FirstOrDefault(p => p.Id == projId);
        }

        public async Task CreateProjectAsync(Project proj)
        {
            _db.Projects.Add(proj);
            await _db.SaveChangesAsync();
        }

        public List<Project> GetProjects()
        {
            return _db.Projects
                .Include(p => p.Tasks)
                .ToList();
        }

        public async Task UpdateProjectAsync(Project proj)
        {
            _db.Projects.Update(proj);
            await _db.SaveChangesAsync();
        }

        public async Task AssignTaskToProjectAsync(int projId, AppTask task)
        {
            Project proj = GetProject(projId);

            if(proj != null)
            {
                proj.Tasks.Add(task);
                _db.Projects.Update(proj);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Unknown error occured");
            }
        }

        public int GetProjectProgress(int projId)
        {
            var project = _db.Projects
                .Include(p => p.Tasks)
                .FirstOrDefault(p => p.Id == projId);            

            if(project != null && project.Tasks.Count != 0)
            {
                int sum = project.Tasks.Sum(t => t.Progress);
                return sum / project.Tasks.Count;
            }

            return 0;
        }
    }
}
