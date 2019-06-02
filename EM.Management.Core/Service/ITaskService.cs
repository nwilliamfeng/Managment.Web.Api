using EM.Management.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Service
{
    public interface ITaskService
    {
        Task<JsonResultData<TaskModel>> AddOrUpdate(TaskModel task);

        Task<JsonResultData< QueryResult<TaskModel>>> GetTasks(TaskQueryCondition conditon);

        Task<JsonResultData<TaskModel>> Load(string taskId);
    }
}
