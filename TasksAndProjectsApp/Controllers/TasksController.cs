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
    [Route("dashboard/tasks")]
    public class TasksController : Controller
    {
        private readonly ITaskManager _taskManager;
        private readonly IAuthManager _authManager;
        private readonly IProjectManager _projManager;

        public TasksController(ITaskManager taskManager, IAuthManager authManager, IProjectManager projectManager)
        {
            _taskManager = taskManager;
            _authManager = authManager;
            _projManager = projectManager;
        }

        [ValidateAntiForgeryToken]
        [HttpPost("delete/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            int projId = (int)TempData["projId"]; // ViewEditProject method
            _taskManager.DeleteTask(taskId);
            return Redirect("/dashboard/projects/" + projId);
        }

        [HttpGet("create")]
        public IActionResult ViewCreateTask()
        {
            if (_authManager.UserIsAuthenticated())
            {
                return View();
            }

            return View("NotAuthorized");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask(CreateTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO add task to db
                int projId = (int)TempData["projId"];
                AppTask task = new AppTask
                {
                    Description = model.Description,
                    Deadline = model.Deadline,
                    Project = _projManager.GetProject(projId)
                };

                await _taskManager.CreateTaskAsync(task);
                await _projManager.AssignTaskToProjectAsync(projId, task);                
                return Redirect("/dashboard/projects/" + projId);
            }

            return View("Error");
        }
    }
}
