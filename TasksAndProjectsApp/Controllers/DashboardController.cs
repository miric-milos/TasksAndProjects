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
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        private readonly IAuthManager _authManager;
        private readonly IProjectManager _projManager;
        private readonly IUserManager _userManager;

        public DashboardController(IAuthManager authManager, IProjectManager projectManager, IUserManager userManager)
        {
            _authManager = authManager;
            _projManager = projectManager;
            _userManager = userManager;
        }

        public IActionResult Dashboard()
        {
            AppUser user = _authManager.GetAuthenticatedUser();

            if(user == null)
            {
                return Redirect("/");
            }
            
            return View(user);
        }

        [HttpGet("projects")]
        public IActionResult Projects()
        {
            AppUser user = _authManager.GetAuthenticatedUser();

            if (user == null) return Redirect("/");

            if(user.Role != Role.Developer) // all users except developers can manage projects
            {
                ListProjectsViewModel model = new ListProjectsViewModel
                {
                    UserRole = user.Role,
                    Projects = _projManager.GetProjects()
                };

                return View(model);
            }

            // inform user of unauthorized actions
            return View("NotAuthorized");
        }

        [HttpGet("users")]
        public IActionResult Users()
        {
            if (_authManager.UserIsAuthenticated())
            {
                var users = _userManager.GetUsers();

                return View(users);
            }

            return View("NotAuthorized");
        }
    }
}
