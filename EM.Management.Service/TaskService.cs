using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
using EM.Management.Data;

namespace EM.Management.Service
{
    public class TaskService : ITaskService
    {
        private IEnumerable<ITaskRepository> _taskRepositories;

        public TaskService(IEnumerable<ITaskRepository> taskRepositories)
        {
            if (taskRepositories == null || taskRepositories.Count() < 2)
                throw new ArgumentException("请配置正确的TaskRepository。");
            this._taskRepositories = taskRepositories;
        }

        private ITaskRepository Cache => this._taskRepositories.FirstOrDefault(x => x.IsCache);

        private ITaskRepository Db => this._taskRepositories.FirstOrDefault(x => !x.IsCache);


        public async Task<JsonResultData<TaskModel>> AddOrUpdate(TaskModel task)
        {
            var result = await this.Cache.AddOrUpdateTask(task);
            result = await this.Db.AddOrUpdateTask(task);
            return result.ToJson();
        }

        public async Task<JsonResultData< QueryResult<TaskModel>>> GetTasks(TaskQueryCondition conditon)
        {
            var result =await this.Db.GetTasks(conditon);
            return result.ToJson();
        }

        public async Task<JsonResultData<TaskModel>> Load(string taskId)
        {
            var result =await this.Cache.Load(taskId);
            return result.ToJson();
        }
    }
}
