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

        public DashboardController(IAuthManager authManager)
        {
            _authManager = authManager;
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
                    Projects = Project.Projects
                };

                return View(model);
            }

            // inform user of unauthorized actions
            return View("NotAuthorized");
        }
    }
}
