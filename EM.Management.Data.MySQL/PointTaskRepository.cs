using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Data;
using EM.Management.Model;

namespace EM.Management.Data.MySQL
{
    public class PointTaskRepository : IPointTaskRepository
    {
        private List<PointTask> _cache = new List<PointTask>();

        public bool IsCache => false;

        public PointTaskRepository()
        {
            for (var i = 0; i < 35; i++)
            {
                _cache.Add(new PointTask { TaskID = $"{i}", TaskExpireDays = 10, CreateTime = DateTime.Now, BeginTime = DateTime.Now, Name = $"任务{i}", PlatformID = 1 });
            }

        }

        public Task<PointTask> AddOrUpdateTask(PointTask task)
        {
            return Task.Run(() =>
            {
                if (_cache.Contains(task))
                    _cache[_cache.IndexOf(task)] = task;
                else
                    _cache.Add(task);
                return task;
            });
        }


        public Task<QueryResult<PointTask>> GetTasks(TaskQueryCondition conditon)
        {
            return Task.Run(() =>
            {
                Func<PointTask, bool> pred = x =>
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
                return new QueryResult<PointTask> { Items = lst.Skip((conditon.PageIndex - 1) * conditon.PageSize).Take(conditon.PageSize).ToList(), TotalCount = lst.Count };
            });
        }

        public Task<PointTask> Load(string taskId)
        {
            return Task.Run(()=> this._cache.FirstOrDefault(x => x.TaskID == taskId));
        }
    }
}
