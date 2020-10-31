using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public interface ITaskManager
    {
        Task DeleteTaskAsync(int taskId);
        Task CreateTaskAsync(AppTask task);
        AppTask GetTask(int taskId);
    }
}
