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

        public void DeleteProject(int projId)
        {
            throw new NotImplementedException();
        }

        public Project GetProjectById(int projId)
        {
            throw new NotImplementedException();
        }

        public void CreateProject(Project proj)
        {
            throw new NotImplementedException();
        }
    }
}
