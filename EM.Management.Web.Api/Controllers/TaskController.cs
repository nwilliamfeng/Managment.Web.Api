 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EM.Management.Service;
using EM.Management.Model;

namespace EM.Management.Web.Controllers
{

    [Authentication]
    public class TaskController : ApiController
    {
        private IPointTaskService _taskService;
        private IPlatformService _platformService;
        private IPointTaskTagService _taskTagService;

        public TaskController(IPointTaskService taskService,IPlatformService platformService,IPointTaskTagService taskTagService)
        {
            this._taskService = taskService;
            this._platformService = platformService;
            this._taskTagService = taskTagService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddOrUpdateTask([FromBody]PointTask task)
        {
            var result =await  this._taskService.AddOrUpdate(task);
            return this.JsonResult(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetTasks([FromBody]TaskQueryCondition queryCondition)
        {
            var result = await this._taskService.GetTasks(queryCondition);
            return this.JsonResult(result);
        }






        //public ActionResult GetPlatforms()
        //{
        //    List<PlatformModel> lst = new List<PlatformModel>();
        //    lst.Add(new PlatformModel { PlatformID = 0, Name = "全部" });
        //    lst.Add(new PlatformModel { PlatformID = 1, Name = "基金" });
        //    lst.Add(new PlatformModel { PlatformID = 2, Name = "证券" });
        //    return lst.ToJsonActionResult();
        //}

        public async Task<IHttpActionResult> GetTaskTags()
        {
            List<TaskTagModel> lst = new List<TaskTagModel>();
            lst.Add(new TaskTagModel { PlatformID = 0, TagName = "标签一", Id = 1 });
            lst.Add(new TaskTagModel { PlatformID = 0, TagName = "标签二", Id = 2 });
            lst.Add(new TaskTagModel { PlatformID = 0, TagName = "标签三", Id = 3 });
            return lst.ToJsonActionResult();
        }
    }
}
