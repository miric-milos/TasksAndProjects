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
        private readonly IAuthManager _authManager;

        public UsersController(IUserManager userManager, ITaskManager taskManager, IAuthManager authManager)
        {
            _userManager = userManager;
            _taskManager = taskManager;
            _authManager = authManager;
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

        [ValidateAntiForgeryToken]
        [HttpPost("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (_authManager.UserIsAuthenticated())
            {
                await _taskManager.UnassignFromTaskAsync(userId);
                await _userManager.DeleteUserAsync(userId);
                return Redirect("/dashboard/users");
            }

            return View("NotAuthorized");
        }

        [HttpGet("{userId}")]
        public IActionResult ViewEditUser(int userId)
        {
            var user = _userManager.GetUser(userId);

            if(user != null)
            {
                TempData["userId"] = userId;
                var model = new EditUserViewModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                    UserName = user.UserName
                };

                return View(model);
            }

            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit")]
        public async Task<IActionResult> UpdateUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUser((int)TempData["userId"]);

                if(user != null)
                {
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Role = model.Role;

                    await _userManager.UpdateUserAsync(user);
                    return Redirect("/dashboard/users");
                }

                return View("Error");
            }

            return View("ViewEditUser");
        }
    }
}
