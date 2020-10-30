using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksAndProjectsApp.Infrastructure;
using TasksAndProjectsApp.Models.ViewModels;

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

        [HttpGet("{projId}")]
        public IActionResult ViewEditProject(int projId)
        {
            var proj = _projectManager.GetProjectById(projId);

            if(proj != null)
            {
                return View(new EditProjectViewModel { Name = proj.Name });
            }

            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("delete/{projId}")]
        public IActionResult DeleteProject(int projId)
        {
            _projectManager.DeleteProject(projId);
            return Redirect("/dashboard/projects");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit")]
        public IActionResult EditProject(EditProjectViewModel model)
        {
            return null;
        }
    }
}
