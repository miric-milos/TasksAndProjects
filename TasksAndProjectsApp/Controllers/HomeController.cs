﻿using System;
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
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = AppUser.Users
                    .FirstOrDefault(u => u.UserName == model.UserName &&
                    u.Password.GetHashCode() == u.Password.GetHashCode());

                if(user != null)
                {
                    // authmanager.login(user)
                }
            }            
        }
    }
}
