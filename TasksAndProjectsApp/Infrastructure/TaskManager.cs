using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAndProjectsApp.Models;

namespace TasksAndProjectsApp.Infrastructure
{
    public class TaskManager : ITaskManager
    {
        private readonly DatabaseContext _db;

        public TaskManager(DatabaseContext db)
        {
            _db = db;
        }

        public async Task CreateTaskAsync(AppTask task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
        }

        public void DeleteTask(int taskId)
        {
            // TODO: create db
        }
    }
}
