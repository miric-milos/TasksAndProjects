using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TasksAndProjectsApp.Infrastructure;
using TasksAndProjectsApp.Models;
using TasksAndProjectsApp.Models.ViewModels;

namespace TasksAndProjectsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthManager _authManager;

        public HomeController(ILogger<HomeController> logger, IAuthManager authManager)
        {
            _logger      = logger;
            _authManager = authManager;
        }

        public IActionResult Index()
        {
            if (_authManager.UserIsAuthenticated()) 
                return Redirect("/dashboard");            

            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                // TODO: load user from DB
                AppUser user = AppUser.Users
                    .FirstOrDefault(u => u.UserName == model.UserName &&
                    u.Password.GetHashCode() == model.Password.GetHashCode());

                if(user != null)
                {
                    _authManager.LogIn(user.Id, model.RememberMe);
                    // TODO: check roles
                    return Redirect("/dashboard");
                }

                // user not found
                ModelState.AddModelError("", "Username or password are incorrect.");
            }

            return View("Index");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public IActionResult LogOut()
        {
            if (_authManager.UserIsAuthenticated())
            {
                _authManager.LogOut();
                return Redirect("/");
            }

            throw new Exception("Unknown error occured!");
        }
    }
}
