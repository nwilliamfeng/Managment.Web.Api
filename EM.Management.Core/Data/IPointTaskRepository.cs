using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;

namespace EM.Management.Data
{
    public interface IPointTaskRepository:IRepository
    {
        Task<QueryResult<PointTask>> GetTasks(TaskQueryCondition conditon);

        Task<PointTask> AddOrUpdateTask(PointTask task);

        Task<PointTask> Load(string taskId);

    }
}
