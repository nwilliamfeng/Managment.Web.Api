using EM.Management.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Service
{
    public interface IPointTaskService
    {
        Task<JsonResultData<PointTask>> AddOrUpdate(PointTask task);

        Task<JsonResultData< QueryResult<PointTask>>> GetTasks(TaskQueryCondition conditon);

        Task<JsonResultData<PointTask>> Load(string taskId);
    }
}
