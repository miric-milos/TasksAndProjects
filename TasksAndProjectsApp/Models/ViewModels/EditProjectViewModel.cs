using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models.ViewModels
{
    public class EditProjectViewModel
    {
        [Required(ErrorMessage = "Project name is required!")]
        public string Name { get; set; }

        // prop tasks
    }
}
