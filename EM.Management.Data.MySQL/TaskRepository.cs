using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Data;
using EM.Management.Model;

namespace EM.Management.Data.MySQL
{
    public class TaskRepository : ITaskRepository
    {
        private List<TaskModel> _cache = new List<TaskModel>();

        public bool IsCache => false;

        public TaskRepository()
        {
            for (var i = 0; i < 35; i++)
            {
                _cache.Add(new TaskModel { TaskID = $"{i}", TaskExpireDays = 10, CreateTime = DateTime.Now, BeginTime = DateTime.Now, Name = $"任务{i}", PlatformID = 1 });
            }

        }

        public Task<bool> AddOrUpdateTask(TaskModel task)
        {
            return Task.Run(() =>
            {
                if (_cache.Contains(task))
                    return false;
                _cache.Add(task);
                return true;
            });
        }


        public Task<QueryResult<TaskModel>> GetTasks(TaskQueryCondition conditon)
        {
            return Task.Run(() =>
            {
                Func<TaskModel, bool> pred = x =>
                {
                    var result = true;
                    if (conditon.EndTime > conditon.StartTime)
                        result = x.BeginTime >= conditon.StartTime && x.BeginTime <= conditon.EndTime;
                    if (!string.IsNullOrEmpty(conditon.TaskName))
                        result = result && x.Name == conditon.TaskName;
                    if (conditon.TaskTagId > 0)
                        result = result && conditon.TaskTagId == x.TagId;
                    return result;
                };
                var lst = this._cache.Where(pred).ToList();
                return new QueryResult<TaskModel> { Items = lst.Skip((conditon.PageIndex - 1) * conditon.PageSize).Take(conditon.PageSize).ToList(), TotalCount = lst.Count };
            });
        }
    }
}
