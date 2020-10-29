using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksAndProjectsApp.Infrastructure;
using TasksAndProjectsApp.Models;

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

            return View();
        }
    }
}
