﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksAndProjectsApp.Infrastructure;
using TasksAndProjectsApp.Models.ViewModels;

namespace TasksAndProjectsApp.Controllers
{
    [Route("dashboard/tasks")]
    public class TasksController : Controller
    {
        private readonly ITaskManager _taskManager;
        private readonly IAuthManager _authManager;

        public TasksController(ITaskManager taskManager, IAuthManager authManager)
        {
            _taskManager = taskManager;
            _authManager = authManager;
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
        public IActionResult CreateTask(CreateTaskViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return null;
        }
    }
}
