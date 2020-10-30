using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksAndProjectsApp.Infrastructure;

namespace TasksAndProjectsApp.Controllers
{
    [Route("dashboard/tasks")]
    public class TasksController : Controller
    {
        private readonly ITaskManager _taskManager;

        public TasksController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        [ValidateAntiForgeryToken]
        [HttpPost("delete/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            int projId = (int)TempData["projId"]; // ViewEditProject method
            _taskManager.DeleteTask(taskId);
            return Redirect("/dashboard/projects/" + projId);
        }
    }
}
