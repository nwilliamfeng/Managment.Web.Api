 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EM.Management.Service;

namespace EM.Management.Web.Controllers
{
    public class TaskController : ApiController
    {
        private ITaskService _taskService;
        private IPlatformService _platformService;

        public TaskController(ITaskService taskService,IPlatformService platformService)
        {
            this._taskService = taskService;
            this._platformService = platformService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddOrUpdateTask([FromBody]TaskModel task)
        {
            var result =await  this._taskService.AddOrUpdate(task);
            return this.JsonResult(JsonData.Success());
        }



        //public ActionResult LoadTasks(int platformId, string startTime, string endTime, int tagId = 0, string taskName = "", int pageIndex = 1, int pageSize = 20)
        //{
        //    var result = TaskCore.GetTaskList(platformId, startTime, endTime, tagId, taskName, pageIndex, pageSize);
        //    return result.ToJsonActionResult();
        //}

         

        //public ActionResult GetPlatforms()
        //{
        //    List<PlatformModel> lst = new List<PlatformModel>();
        //    lst.Add(new PlatformModel { PlatformID = 0, Name = "全部" });
        //    lst.Add(new PlatformModel { PlatformID = 1, Name = "基金" });
        //    lst.Add(new PlatformModel { PlatformID = 2, Name = "证券" });
        //    return lst.ToJsonActionResult();
        //}

        //public ActionResult GetTaskTags()
        //{
        //    List<TaskTagModel> lst = new List<TaskTagModel>();
        //    lst.Add(new TaskTagModel { PlatformID = 0, TagName = "标签一", Id = 1 });
        //    lst.Add(new TaskTagModel { PlatformID = 0, TagName = "标签二", Id = 2 });
        //    lst.Add(new TaskTagModel { PlatformID = 0, TagName = "标签三", Id = 3 });
        //    return lst.ToJsonActionResult();
        //}
    }
}
