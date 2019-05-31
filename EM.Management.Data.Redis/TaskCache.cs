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
    public class TaskCache:RedisCache,ITaskRepository
    {
        private const string KEY = "hash_tasks";

        public async Task<bool> AddOrUpdateTask(TaskModel task)
        {
            if (task.IsNew)
                task.TaskID = Guid.NewGuid().ToString("N");
            else if (!this.Database.HashExists(KEY, task.TaskID))
                throw new InvalidOperationException("不存在的task");
            return await this.Database.HashSetAsync(KEY, task.TaskID, JsonConvert.SerializeObject(task));
        }

        public async Task<IEnumerable<PlatformModel>> GetPlatforms()
        {
            var values = await this.Database.SetMembersAsync(KEY);
            return values.Select(x => JsonConvert.DeserializeObject<PlatformModel>(x));
        }

        public Task<QueryResult<TaskModel>> GetTasks(TaskQueryCondition conditon)
        {
           if(conditon.)
        }
    }
}
