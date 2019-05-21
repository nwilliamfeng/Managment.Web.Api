using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EM.Management.API.Controllers
{
    public class PlatformController : ApiController
    {
        public  JsonResult<List<PlatformModel>> GetPlatformList()
        {
            var lst = new List<PlatformModel>();
            lst.Add(new PlatformModel { PlatformID = 0, Name = "全部平台", CreateTime = DateTime.Now, IsEnabled = 1 });
            lst.Add(new PlatformModel { PlatformID = 1, Name = "基金", CreateTime = DateTime.Now, IsEnabled = 1 });
            lst.Add(new PlatformModel { PlatformID = 2, Name = "证券", CreateTime = DateTime.Now, IsEnabled = 1 });
            return new JsonResult<List<PlatformModel>> { Data = lst, Count = lst.Count, StatusCode = 1 };
        }
    }
}
