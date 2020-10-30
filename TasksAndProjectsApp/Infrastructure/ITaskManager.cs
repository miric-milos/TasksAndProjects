using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Infrastructure
{
    public interface ITaskManager
    {
        void DeleteTask(int taskId);
    }
}
