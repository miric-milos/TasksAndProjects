using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksAndProjectsApp.Infrastructure;

namespace TasksAndProjectsApp.Controllers
{
    [Route("dashboard/projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectManager _projectManager;

        public ProjectsController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [ValidateAntiForgeryToken]
        [HttpPost("delete/{projId}")]
        public IActionResult DeleteProject(int projId)
        {
            _projectManager.DeleteProject(projId);
            return Redirect("/dashboard/projects");
        }
    }
}
