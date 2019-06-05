
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using EM.Management.Service;
using EM.Management.Model;
using WebApi.OutputCache.V2;
using System.Web.Http.Controllers;

namespace EM.Management.Web.Controllers
{
    [Authentication]
    public class TaskController : ApiController
    {
        private IPointTaskService _taskService;
        private IPointTaskTagService _taskTagService;

        public TaskController(IPointTaskService taskService ,IPointTaskTagService taskTagService)
        {
            this._taskService = taskService;
            this._taskTagService = taskTagService;
        }


        
        [HttpPost]
        public async Task<IHttpActionResult> AddOrUpdateTask([FromBody]PointTask task)
        {
            var result =await  this._taskService.AddOrUpdate(task);
            return this.JsonResult(result);
        }


        [HttpPost]
        [CacheOutputWithPostAttribute(ServerTimeSpan = 22)]
        public async Task<IHttpActionResult> Tasks([FromBody]TaskQueryCondition qc)
        {
            var result = await this._taskService.GetTasks(qc);
            return this.JsonResult(result);
        }




        [HttpGet]
        [CacheOutput(ServerTimeSpan = 10)]
        public async Task<IHttpActionResult> TaskTags()
        {
            var result = await this._taskTagService.LoadAll();
            return this.JsonResult(result);
        }
    }
}
