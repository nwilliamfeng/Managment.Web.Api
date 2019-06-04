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
    [Authentication]
    public class PlatformController : ApiController
    {
        private IPlatformService _platformService;

        public PlatformController(IPlatformService platformService)
        {
            this._platformService = platformService;
        }

        //public async Task<JsonResult<List<PlatformModel>>> GetPlatformList()
        //{
        //    var lst = await this._platformService.GetPlatforms();
        //    return new JsonResult<List<PlatformModel>> { Data = lst.ToList(), Count = lst.Count(), StatusCode = 1 };         
        //}


        public async Task<IHttpActionResult> GetPlatformList()
        {
            var lst = await this._platformService.GetPlatforms();        
            return this.JsonResult(lst.ToJsonResultData());
        }

      
    }
}
