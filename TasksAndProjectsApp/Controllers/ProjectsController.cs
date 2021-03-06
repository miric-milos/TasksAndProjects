﻿using System;
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
        public async Task<IActionResult> CreateProject(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var proj = new Project { Name = model.Name };
                await _projectManager.CreateProjectAsync(proj);
                return Redirect("/dashboard/projects");
            }

            return View("ViewCreateProject");
        }

        [HttpGet("{projId}")]
        public IActionResult ViewEditProject(int projId)
        {
            var proj = _projectManager.GetProject(projId);

            if(proj != null)
            {
                TempData["projId"] = projId;
                return View(new EditProjectViewModel { Name = proj.Name, Tasks = proj.Tasks });
            }

            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("delete/{projId}")]
        public async Task<IActionResult> DeleteProject(int projId)
        {
            await _projectManager.DeleteProjectAsync(projId);
            return Redirect("/dashboard/projects");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit")]
        public async Task<IActionResult> EditProject(EditProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                int projId = (int)TempData["projId"]; // see ViewEditProject method

                var proj = _projectManager.GetProject(projId);

                if(proj != null)
                {
                    proj.Name = model.Name;
                    await _projectManager.UpdateProjectAsync(proj);
                    return Redirect("/dashboard/projects");
                }

                return View("ViewEditProject");
            }

            return View("ViewEditProject");
        }
    }
}
