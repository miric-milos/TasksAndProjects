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

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = GetTask(taskId);

            if(task != null)
            {
                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Unknown error occured!");
            }
        }

        public AppTask GetTask(int taskId)
        {
            return _db.Tasks
                .FirstOrDefault(t => t.Id == taskId);
        }
    }
}
