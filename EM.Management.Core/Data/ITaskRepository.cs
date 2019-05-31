using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;

namespace EM.Management.Data
{
    public interface ITaskRepository:IRepository
    {
        Task<QueryResult<TaskModel>> GetTasks(TaskQueryCondition conditon);

        Task<bool> AddOrUpdateTask(TaskModel task);
 
    }
}
