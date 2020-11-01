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
    [Route("dashboard/users")]
    public class UsersController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly ITaskManager _taskManager;

        public UsersController(IUserManager userManager, ITaskManager taskManager)
        {
            _userManager = userManager;
            _taskManager = taskManager;
        }

        [ValidateAntiForgeryToken]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = model.Role,
                    UserName = model.UserName                    
                };

                await _userManager.CreateUserAsync(user, model.Password);                
            }
            else
            {
                TempData["createUserErrorMessage"] = 
                    "Something went wrong. Either some fields were empty or data formats were invalid, please try again.";
            }

            return Redirect("/dashboard/users");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("assign/{userId}")]
        public async Task<IActionResult> AssignTaskToUser(int userId)
        {
            var task = _taskManager.GetTask((int)TempData["taskId"]);
            var user = _userManager.GetUser(userId);

            if(task != null && user != null)
            {
                user.Tasks.Add(task);
                task.Assignee = user;

                await _userManager.UpdateUserAsync(user);
                await _taskManager.UpdateTaskAsync(task);

                return Redirect("/dashboard/projects/" + task.Project.Id);
            }

            return null;
        }
    }
}
