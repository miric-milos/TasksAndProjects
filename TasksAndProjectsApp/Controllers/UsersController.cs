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
        private readonly IUserManager _usermanager;

        public UsersController(IUserManager usermanager)
        {
            _usermanager = usermanager;
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

                await _usermanager.CreateUserAsync(user, model.Password);                
            }
            else
            {
                TempData["createUserErrorMessage"] = 
                    "Something went wrong. Either some fields were empty or data formats were invalid, please try again.";
            }
            return Redirect("/dashboard/users");
        }
    }
}
