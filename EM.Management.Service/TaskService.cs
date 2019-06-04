using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
using EM.Management.Data;

namespace EM.Management.Service
{
    public class TaskService : IPointTaskService
    {
        private IEnumerable<IPointTaskRepository> _taskRepositories;

        public TaskService(IEnumerable<IPointTaskRepository> taskRepositories)
        {
            if (taskRepositories == null || taskRepositories.Count() < 2)
                throw new ArgumentException("请配置正确的TaskRepository。");
            this._taskRepositories = taskRepositories;
        }

        private IPointTaskRepository Cache => this._taskRepositories.FirstOrDefault(x => x.IsCache);

        private IPointTaskRepository Db => this._taskRepositories.FirstOrDefault(x => !x.IsCache);


        public async Task<JsonResultData<PointTask>> AddOrUpdate(PointTask task)
        {
            var result = await this.Cache.AddOrUpdateTask(task);
            result = await this.Db.AddOrUpdateTask(task);
            return result.ToJsonResultData();
        }

        public async Task<JsonResultData< QueryResult<PointTask>>> GetTasks(TaskQueryCondition conditon)
        {
            var result =await this.Db.GetTasks(conditon);
            return result.ToJsonResultData();
        }

        public async Task<JsonResultData<PointTask>> Load(string taskId)
        {
            var result =await this.Cache.Load(taskId);
            return result.ToJsonResultData();
        }
    }
}
