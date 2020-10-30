using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksAndProjectsApp.Infrastructure;
using TasksAndProjectsApp.Models;
using TasksAndProjectsApp.Models.ViewModels;

namespace TasksAndProjectsApp.Controllers
{
    [Route("dashboard/projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectManager _projectManager;
        private readonly IAuthManager _authManager;

        public ProjectsController(IProjectManager projectManager, IAuthManager authManager)
        {
            _projectManager = projectManager;
            _authManager = authManager;
        }

        [HttpGet("create")]
        public IActionResult ViewCreateProject()
        {
            if (_authManager.UserIsAuthenticated())
            {
                return View();
            }
            return View("NotAuthorized");
        }

        [HttpPost("create")]
        public IActionResult CreateProject(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var proj = new Project { Id = 2, Name = model.Name, Tasks = new List<Models.Task>() };
                _projectManager.CreateProject(proj);
                return Redirect("/dashboard/projects");
            }

            return View("ViewCreateProject");
        }

        [HttpGet("{projId}")]
        public IActionResult ViewEditProject(int projId)
        {
            var proj = _projectManager.GetProjectById(projId);

            if(proj != null)
            {
                TempData["projId"] = projId;
                return View(new EditProjectViewModel { Name = proj.Name, Tasks = proj.Tasks });
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
            int projId = (int)TempData["projId"]; // see ViewEditProject method

            var proj = _projectManager.GetProjectById(projId);

            if(proj != null)
            {
                proj.Name = model.Name;
                return Redirect("/dashboard/projects");
            }

            return View("ViewEditProject");
        }
    }
}
