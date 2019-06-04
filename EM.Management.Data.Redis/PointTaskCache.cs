using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace EM.Management.Data.Redis
{
    public class PointTaskCache : RedisCache, IPointTaskRepository
    {
        private const string KEY = "hash_tasks";

        public async Task<PointTask> AddOrUpdateTask(PointTask task)
        {
            var action = task.IsNew ? "create" : "modify";
            if (task.IsNew)
                task.TaskID = Guid.NewGuid().ToString("N");
            else if (!this.Database.HashExists(KEY, task.TaskID))
                throw new InvalidOperationException("不存在的task");
            var result = await this.Database.HashSetAsync(KEY, task.TaskID, JsonConvert.SerializeObject(task));
            $"{action} task : {Newtonsoft.Json.JsonConvert.SerializeObject(task)} , the result is {result}".Log();
            return task;

        }

      
        public Task<QueryResult<PointTask>> GetTasks(TaskQueryCondition conditon)
        {
            return Task.Run(() => new QueryResult<PointTask>());
        }

        public async Task<PointTask> Load(string taskId)
        {
            var value = await this.Database.HashGetAsync(KEY, taskId);
            if (!string.IsNullOrEmpty(value))
                return JsonConvert.DeserializeObject<PointTask>(value);
            return null;

        }
    }
}
