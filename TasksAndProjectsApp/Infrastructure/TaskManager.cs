using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Infrastructure
{
    public class TaskManager : ITaskManager
    {
        private readonly DatabaseContext _db;

        public TaskManager(DatabaseContext db)
        {
            _db = db;
        }

        public void DeleteTask(int taskId)
        {
            // TODO: create db
        }
    }
}
