using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models.ViewModels
{
    public class EditTaskViewModel
    {
        [Required(ErrorMessage = "Description required!")]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int Progress { get; set; }
    }
}
