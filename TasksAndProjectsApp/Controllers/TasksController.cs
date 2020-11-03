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
        private readonly IUserManager _userManager;

        public TasksController(ITaskManager taskManager, IAuthManager authManager, 
            IProjectManager projectManager, IUserManager userManager)
        {
            _taskManager = taskManager;
            _authManager = authManager;
            _projManager = projectManager;
            _userManager = userManager;
        }

        [HttpGet("")]
        public IActionResult YourTasksView()
        {
            var user = _authManager.GetAuthenticatedUser();
            if (user != null)
            {
                IEnumerable<AppTask> tasks = _taskManager.GetTasksForAuthUser(user.Id);
                return View(tasks);
            }

            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("delete/{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            int projId = (int)TempData["projId"]; // ViewEditProject method
            await _taskManager.DeleteTaskAsync(taskId);
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

        [HttpGet("{taskId}")]
        public IActionResult ViewEditTask(int taskId)
        {
            AppTask task = _taskManager.GetTask(taskId);

            if(task != null)
            {
                TempData["taskId"] = task.Id;

                var model = new EditTaskViewModel
                {
                    Description = task.Description,
                    Progress = task.Progress,
                    Status = task.Status,
                    Deadline = task.Deadline,
                };

                return View(model);
            }

            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit")]
        public async Task<IActionResult> UpdateTask(EditTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                int taskId = (int)TempData["taskId"];
                var task = _taskManager.GetTask(taskId);

                if(task != null)
                {
                    task.Description = model.Description;
                    task.Deadline = model.Deadline;
                    task.Progress = model.Progress;
                    task.Status = model.Status;

                    await _taskManager.UpdateTaskAsync(task);

                    return Redirect("/dashboard/projects/" + task.Project.Id);
                }

                return View("Error");
            }

            return View("ViewEditTask");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit_assigned")]
        public async Task<IActionResult> UpdateAssignedTask(EditTaskViewModel model)
        {
            // get id of task
            if (ModelState.IsValid)
            {
                var task = _taskManager.GetTask(model.Id);

                if(task != null)
                {
                    task.Description = model.Description;
                    task.Progress = model.Progress;
                    task.Status = model.Status;

                    await _taskManager.UpdateTaskAsync(task);
                    return Redirect("/dashboard/tasks");
                }
                return View("Error");
            }
            TempData[model.Id + "_errorMessage"] = "Some fields were invalid, or empty, please try again";
            return Redirect("/dashboard/tasks");
        }

        [HttpGet("assign/{taskId}")]
        public IActionResult AssignTaskView(int taskId)
        {
            var user = _authManager.GetAuthenticatedUser();

            if (user != null)
            {
                TempData["taskId"] = taskId;
                // if user is admin, he can assign a task to anybody, PMs can only assign to developers
                IEnumerable<AppUser> users =
                    user.Role == Role.Administrator ? _userManager.GetUsers() : _userManager.GetUsers(Role.Developer);

                return View(users);
            }

            return View("NotAuthorized");
        }
    }
}