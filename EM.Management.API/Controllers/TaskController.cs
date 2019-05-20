using EM.Management.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EM.Management.API.Controllers
{
    public class TaskController : ApiController
    {
 
        [HttpPost]
        public bool AddTask([FromBody]TaskModel task)
        {
            Console.WriteLine(task);
            throw new Exception("throw exeception");

            return true;
        }

       

        //public ActionResult LoadTasks(int pageSize = 10, int pageIndex = 1)
        //{
        //    var tasks = new List<TaskModel>();
        //    for (int i = 1; i < 23; i++)
        //    {
        //        tasks.Add(new TaskModel { TaskID = i.ToString(), BeginTime = DateTime.Now.ToString("yyyy-MM-dd"), EndTime = DateTime.Now.ToString("yyyy-MM-dd"), Description = "desc1", Name = "task" + i.ToString(), CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
        //    }
        //    return new JsonResult<List<TaskModel>> { data = tasks.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(), count = tasks.Count, }.ToJsonActionResult();

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
