using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
