using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Models.ViewModels
{
    public class EditTaskViewModel
    {
        [ReadOnly(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Description required!")]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Progress cannot exceed 100%")]
        public int Progress { get; set; }
    }
}
