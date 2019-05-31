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
        Task<bool> AddOrUpdate(TaskModel task);

        Task<QueryResult<TaskModel>> GetTasks(TaskQueryCondition conditon);
    }
}
